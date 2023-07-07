using Assimp;
using Kotono.File;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using NAudio.Wave;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Kotono.Graphics.Objects.Meshes
{
    public abstract class Mesh : IDisposable
    {
        private static readonly Dictionary<string, Tuple<int[], Vector, Vector[], Triangle[]>> _paths = new();

        private Transform _transform;

        private Vector _locationVelocity;

        private Vector _rotationVelocity;

        private readonly IHitbox[] _hitboxes;

        protected readonly ShaderType _shaderType;

        public bool IsDraw { get; private set; } = true;

        public bool IsInFront = false;

        public Triangle[] Triangles { get; }

        public Vector[] Vertices { get; }

        public Vector Center { get; }

        public bool IsFiziks { get; set; }

        public bool IsGravity { get; set; }

        public CollisionState CollisionState { get; set; }

        public int VertexArrayObject { get; set; }

        public int VertexBufferObject { get; }

        public int IndicesCount { get; }

        public int[] Textures { get; }

        public Transform Transform => _transform;

        public Vector Location
        {
            get => _transform.Location;
            set => _transform.Location = value;
        }

        public Vector Rotation
        {
            get => _transform.Rotation;
            set => _transform.Rotation = value;
        }

        public Vector Scale
        {
            get => _transform.Scale;
            set => _transform.Scale = value;
        }

        public Vector LocationVelocity
        {
            get => _locationVelocity;
            set => _locationVelocity = value;
        }

        public Vector RotationVelocity
        {
            get => _rotationVelocity;
            set => _rotationVelocity = value;
        }

        public Color Color { get; set; }

        public Matrix4 Model => Transform.Model;

        public static double MaxIntersectionCheckTime => 0.1f;

        public double IntersectionCheckTime { get; set; } = MaxIntersectionCheckTime;

        private readonly Properties _properties;

        public Mesh(string path, IHitbox[] hitboxes)
        {
            _properties = Properties.Parse(path);

            var textureKeys = _properties.Strings.Keys.Where(k => k.StartsWith("Textures")).ToList();

            if (textureKeys.Count > 32)
            {
                throw new Exception($"error: maximum number of textures for a Mesh is 32");
            }

            Textures = new int[textureKeys.Count];
            for (int i = 0; i < textureKeys.Count; i++)
            {
                Textures[i] = TextureManager.LoadTexture(_properties.Strings[textureKeys[i]]);
            }

            _shaderType = _properties.Strings["ShaderType"] switch
            {
                "Lighting" => ShaderType.Lighting,
                "PointLight" => ShaderType.PointLight,
                "Gizmo" => ShaderType.Gizmo,
                _ => throw new Exception($"error: ShaderType \"{_properties.Strings["ShaderType"]}\" isn't valid"),
            };

            Location = new Vector
            {
                X = _properties.Floats["Transform.Location.X"],
                Y = _properties.Floats["Transform.Location.Y"],
                Z = _properties.Floats["Transform.Location.Z"]
            };

            Rotation = new Vector
            {
                X = _properties.Floats["Transform.Rotation.X"],
                Y = _properties.Floats["Transform.Rotation.Y"],
                Z = _properties.Floats["Transform.Rotation.Z"]
            };

            Scale = new Vector
            {
                X = _properties.Floats["Transform.Scale.X"],
                Y = _properties.Floats["Transform.Scale.Y"],
                Z = _properties.Floats["Transform.Scale.Z"]
            };

            Color = new Color
            {
                R = _properties.Floats["Color.R"],
                G = _properties.Floats["Color.G"],
                B = _properties.Floats["Color.B"],
                A = _properties.Floats["Color.A"]
            };

            _hitboxes = hitboxes;

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Location = Location;
                hitbox.Rotation = Vector.Zero;
                hitbox.Scale = Scale * 2;
                hitbox.Color = Color.Red;
            }

            if (!_paths.ContainsKey(_properties.Strings["Obj"]))
            {
                List<Vertex>[] models;
                List<int>[] indices;
                List<Triangle> triangles = new();

                using (var importer = new AssimpContext())
                {
                    var scene = importer.ImportFile(_properties.Strings["Obj"], PostProcessSteps.Triangulate);

                    foreach (var face in scene.Meshes[0].Faces)
                    {
                        triangles.Add(new Triangle(
                            (Vector)scene.Meshes[0].Vertices[face.Indices[0]],
                            (Vector)scene.Meshes[0].Vertices[face.Indices[1]],
                            (Vector)scene.Meshes[0].Vertices[face.Indices[2]],
                            new Transform(),
                            Color.White
                        ));
                    }

                    models = new List<Vertex>[scene.Meshes.Count];
                    indices = new List<int>[scene.Meshes.Count];
                    for (int i = 0; i < scene.Meshes.Count; i++)
                    {
                        var mesh = scene.Meshes[i];
                        var tempVertices = new List<Vertex>();

                        for (int j = 0; j < mesh.Vertices.Count; j++)
                        {
                            var loc = new Vector(mesh.Vertices[j].X, mesh.Vertices[j].Y, mesh.Vertices[j].Z);
                            var normal = new Vector(mesh.Normals[j].X, mesh.Normals[j].Y, mesh.Normals[j].Z);
                            var texCoord = new Point(mesh.TextureCoordinateChannels[0][j].X, mesh.TextureCoordinateChannels[0][j].Y);

                            tempVertices.Add(new Vertex(loc, normal, texCoord));
                        }

                        models[i] = tempVertices;
                        indices[i] = mesh.GetIndices().ToList();
                    }
                }

                var center = Vector.Zero;
                models[0].ForEach(v => center += v.Location);
                center /= models[0].Count;

                var vertices = new List<Vector>();
                foreach (var vertex in models[0])
                {
                    if (!vertices.Any(v => v == vertex.Location))
                    {
                        vertices.Add(vertex.Location);
                    }
                }

                // create vertex array
                int vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayObject);

                // create vertex buffer
                int vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, models[0].Count * Vertex.SizeInBytes, models[0].ToArray(), BufferUsageHint.StaticDraw);

                int locationAttributeLocation = KT.GetShaderAttribLocation(_shaderType, "aPos");
                GL.EnableVertexAttribArray(locationAttributeLocation);
                GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

                int normalAttributeLocation = KT.GetShaderAttribLocation(_shaderType, "aNormal");
                GL.EnableVertexAttribArray(normalAttributeLocation);
                GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 3);

                int texCoordAttributeLocation = KT.GetShaderAttribLocation(_shaderType, "aTexCoords");
                GL.EnableVertexAttribArray(texCoordAttributeLocation);
                GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 6);

                // create element buffer
                int elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices[0].Count * sizeof(int), indices[0].ToArray(), BufferUsageHint.StaticDraw);

                _paths[_properties.Strings["Obj"]] = Tuple.Create(
                    new int[]
                    {
                        vertexArrayObject,
                        vertexBufferObject,
                        indices[0].Count
                    },
                    center,
                    vertices.ToArray(),
                    triangles.ToArray()
                );
            }

            VertexArrayObject = _paths[_properties.Strings["Obj"]].Item1[0];
            VertexBufferObject = _paths[_properties.Strings["Obj"]].Item1[1];
            IndicesCount = _paths[_properties.Strings["Obj"]].Item1[2];
            Center = _paths[_properties.Strings["Obj"]].Item2;
            Vertices = _paths[_properties.Strings["Obj"]].Item3;
            Triangles = _paths[_properties.Strings["Obj"]].Item4;

            KT.CreateMesh(this);
        }


        public virtual void Init() { }

        public virtual void Update()
        {
            var tempLoc = Location;

            if (IsGravity)
            {
                tempLoc += Fiziks.Gravity * Time.DeltaS;
            }

            if (IsFiziks)
            {
                Fiziks.Update(this);
            }

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Location = tempLoc;

                if ((CollisionState == CollisionState.BlockAll) && hitbox.IsColliding())
                {
                    hitbox.Location = Location;
                }
                else
                {
                    Location = tempLoc;
                }
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < Textures.Length; i++)
            {
                TextureManager.UseTexture(Textures[i], TextureUnit.Texture0 + i);
            }

            KT.SetShaderMatrix4(_shaderType, "model", Model);
            KT.SetShaderColor(_shaderType, "color", Color);
            KT.SetShaderBool(_shaderType, "isInFront", IsInFront);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        public bool IsMouseOn(out Vector intersectionPoint, out float distance)
        {
            foreach (var triangle in Triangles)
            {
                triangle.Transform = Transform;
                if (Intersection.IntersectRayTriangle(KT.ActiveCamera.Location, Mouse.Ray, triangle, out intersectionPoint, out distance))
                {
                    return true;
                }
            }

            intersectionPoint = Vector.Zero;
            distance = 0;
            return false;
        }

        public void Show()
        {
            IsDraw = true;
        }

        public void Hide()
        {
            IsDraw = false;
        }

        public void WriteData()
        {
            _properties.Floats["Color.R"] = Color.R;
            _properties.Floats["Color.G"] = Color.G;
            _properties.Floats["Color.B"] = Color.B;
            _properties.Floats["Color.A"] = Color.A;

            _properties.Floats["Transform.Location.X"] = Location.X;
            _properties.Floats["Transform.Location.Y"] = Location.Y;
            _properties.Floats["Transform.Location.Z"] = Location.Z;

            _properties.Floats["Transform.Rotation.X"] = Rotation.X;
            _properties.Floats["Transform.Rotation.Y"] = Rotation.Y;
            _properties.Floats["Transform.Rotation.Z"] = Rotation.Z;

            _properties.Floats["Transform.Scale.X"] = Scale.X;
            _properties.Floats["Transform.Scale.Y"] = Scale.Y;
            _properties.Floats["Transform.Scale.Z"] = Scale.Z;

            _properties.WriteFile();
        }

        public void Dispose()
        {
            foreach (var hitbox in _hitboxes)
            {
                KT.DeleteHitbox(hitbox);
            }

            GC.SuppressFinalize(this);
        }
    }
}