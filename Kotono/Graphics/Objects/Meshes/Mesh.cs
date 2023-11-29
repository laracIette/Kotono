using Assimp;
using Kotono.File;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Objects.Shapes;
using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Kotono.Graphics.Objects.Meshes
{
    public abstract class Mesh : IObject3D
    {
        private static readonly Dictionary<string, MeshSettings> _paths = new();

        private Vector _locationVelocity;

        private Vector _rotationVelocity;

        private readonly IHitbox[] _hitboxes;

        protected readonly Shader _shader;

        public bool IsDraw { get; private set; } = true;

        public bool IsFiziks { get; set; } = false;

        public bool IsGravity { get; set; } = false;

        public CollisionState CollisionState { get; set; }

        private readonly MeshSettings _meshSettings;

        public int VertexArrayObject => _meshSettings.VertexArrayObject;

        public int VertexBufferObject => _meshSettings.VertexBufferObject;

        public int IndicesCount => _meshSettings.IndicesCount;

        public Vector Center => _meshSettings.Center;

        public Vector[] Vertices => _meshSettings.Vertices;

        public Triangle[] Triangles => _meshSettings.Triangles;

        private readonly Texture[] _textures;

        private Transform _transform;

        public Transform Transform
        {
            get => _transform;
            set => _transform = value;
        }

        public Vector Location
        {
            get => _transform.Location;
            set => _transform.Location = value;
        }

        public Vector Rotation
        {
            get => Vector.Deg(_transform.Rotation);
            set => _transform.Rotation = Vector.Rad(value);
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
            get => Vector.Deg(_rotationVelocity);
            set => _rotationVelocity = Vector.Rad(value);
        }

        public Color Color { get; set; }

        public Matrix4 Model => Transform.Model;

        public static double MaxIntersectionCheckTime => 0.1;

        public double IntersectionCheckTime { get; internal set; } = MaxIntersectionCheckTime;

        private readonly MeshProperties _properties;

        public Mesh(string path, IHitbox[] hitboxes)
        {
            _properties = new MeshProperties(path);

            var textureKeys = _properties.Strings.Keys.Where(k => k.StartsWith("Textures")).ToList();

            if (textureKeys.Count > 32)
            {
                throw new Exception($"error: maximum number of Texture for a Mesh is 32");
            }

            _textures = new Texture[textureKeys.Count];
            for (int i = 0; i < textureKeys.Count; i++)
            {
                _textures[i] = Texture.Load(_properties.Strings[textureKeys[i]], TextureUnit.Texture0 + i);
            }

            _shader = _properties.Strings["Shader"] switch
            {
                "Lighting" => ShaderManager.Lighting,
                "PointLight" => ShaderManager.PointLight,
                "Gizmo" => ShaderManager.Gizmo,
                _ => throw new Exception($"error: Shader \"{_properties.Strings["Shader"]}\" isn't valid"),
            };

            Transform = _properties.Transform;

            Color = _properties.Color;

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
                        triangles.Add(new Triangle(new Vector[]
                            {
                                (Vector)scene.Meshes[0].Vertices[face.Indices[0]],
                                (Vector)scene.Meshes[0].Vertices[face.Indices[1]],
                                (Vector)scene.Meshes[0].Vertices[face.Indices[2]]
                            },
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

                int locationAttributeLocation = _shader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(locationAttributeLocation);
                GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

                int normalAttributeLocation = _shader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalAttributeLocation);
                GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 3);

                int texCoordAttributeLocation = _shader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordAttributeLocation);
                GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 6);

                // create element buffer
                int elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices[0].Count * sizeof(int), indices[0].ToArray(), BufferUsageHint.StaticDraw);

                _paths[_properties.Strings["Obj"]] = new MeshSettings
                {
                    VertexArrayObject = vertexArrayObject,
                    VertexBufferObject = vertexBufferObject,
                    IndicesCount = indices[0].Count,
                    Center = center,
                    Vertices = vertices.ToArray(),
                    Triangles = triangles.ToArray()
                };
            }

            _meshSettings = _paths[_properties.Strings["Obj"]];

            Create();
        }

        protected virtual void Create()
        {
            ObjectManager.Create(this);
        }

        public virtual void Init() { }

        public virtual void Update()
        {
            var tempLoc = Location;

            if (IsGravity)
            {
                tempLoc += Fizix.Gravity * Time.DeltaS;
            }

            if (IsFiziks)
            {
                Fizix.Update(this);
            }

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Location = tempLoc;

                if ((CollisionState == CollisionState.BlockAll) && hitbox.IsColliding)
                {
                    hitbox.Location = Location;
                }
                else
                {
                    Location = tempLoc;
                }
            }

            if (Mouse.IsButtonPressed(MouseButton.Left) && IsMouseOn(out _, out _))
            {
                Gizmo.AttachTo(this);
            }
        }

        public void UpdateShaders() { }

        public virtual void Draw()
        {
            foreach (var texture in _textures)
            {
                texture.Use();
            }

            _shader.SetMatrix4("model", Model);
            _shader.SetColor("color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        public bool IsMouseOn(out Vector intersectionPoint, out float distance)
        {
            foreach (var triangle in Triangles)
            {
                triangle.Transform = Transform;
                if (Intersection.IntersectRayTriangle(CameraManager.ActiveCamera.Location, Mouse.Ray, triangle, out intersectionPoint, out distance))
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

        public void Save()
        {
            WriteData();
        }

        private void WriteData()
        {
            _properties.Transform = Transform;
            _properties.Color = Color;

            _properties.WriteFile();
        }

        public void Delete()
        {
            ObjectManager.Delete(this);
        }

        public void Dispose()
        {
            foreach (var hitbox in _hitboxes)
            {
                hitbox.Delete();
            }

            GC.SuppressFinalize(this);
        }
    }
}