using Assimp;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Physics;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Kotono.Graphics.Objects.Meshes
{
    public class Mesh
    {
        private static readonly Dictionary<string, Tuple<int[], Vector, Vector[]>> _paths = new();

        private Transform _transform;

        private Vector _locationVelocity;

        private Vector _rotationVelocity;

        private readonly IHitbox[] _hitboxes;

        protected readonly ShaderType _shaderType;

        public Mesh(string path, Transform transform, string diffusePath, string specularPath, ShaderType shaderType, Vector color, IHitbox[] hitboxes)
        {
            var diffuseMap = TextureManager.LoadTexture(diffusePath);
            var specularMap = TextureManager.LoadTexture(specularPath);

            if (!_paths.ContainsKey(path))
            {
                List<Vertex>[] models;
                List<int>[] indices;

                using (var importer = new AssimpContext())
                {
                    var scene = importer.ImportFile(path, PostProcessSteps.Triangulate);

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
                            var texCoord = new Vector2(mesh.TextureCoordinateChannels[0][j].X, mesh.TextureCoordinateChannels[0][j].Y);

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

                int locationAttributeLocation = KT.GetShaderAttribLocation(shaderType, "aPos");
                GL.EnableVertexAttribArray(locationAttributeLocation);
                GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

                int normalAttributeLocation = KT.GetShaderAttribLocation(shaderType, "aNormal");
                GL.EnableVertexAttribArray(normalAttributeLocation);
                GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 3);

                int texCoordAttributeLocation = KT.GetShaderAttribLocation(shaderType, "aTexCoords");
                GL.EnableVertexAttribArray(texCoordAttributeLocation);
                GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 6);

                // create element buffer
                int elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices[0].Count * sizeof(int), indices[0].ToArray(), BufferUsageHint.StaticDraw);

                _paths[path] = Tuple.Create(
                    new int[]
                    {
                        vertexArrayObject,
                        vertexBufferObject,
                        indices[0].Count
                    },
                    center,
                    vertices.ToArray()
                );
            }

            VertexArrayObject = _paths[path].Item1[0];
            VertexBufferObject = _paths[path].Item1[1];
            IndicesCount = _paths[path].Item1[2];
            Center = _paths[path].Item2;
            Vertices = _paths[path].Item3;
            Location = transform.Location;
            Rotation = transform.Rotation;
            Scale = transform.Scale;
            DiffuseMap = diffuseMap;
            SpecularMap = specularMap;
            _shaderType = shaderType;
            Color = color;

            _hitboxes = hitboxes;

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Location = Location;
                hitbox.Rotation = Vector.Zero;
                hitbox.Scale = Scale * 2;
                hitbox.Color = Vector.UnitX;
            }
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
            TextureManager.UseTexture(DiffuseMap, TextureUnit.Texture0);
            TextureManager.UseTexture(SpecularMap, TextureUnit.Texture1);

            KT.SetShaderMatrix4(_shaderType, "model", Model);
            KT.SetShaderVector(_shaderType, "color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        public Vector[] Vertices { get; }
        
        public Vector Center { get; }

        public bool IsFiziks { get; set; }

        public bool IsGravity { get; set; }

        public CollisionState CollisionState { get; set; }

        public int VertexArrayObject { get; }

        public int VertexBufferObject { get; }

        public int IndicesCount { get; }

        public int DiffuseMap { get; }

        public int SpecularMap { get; }


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
        
        public Vector Color { get; set; }

        public Matrix4 Model =>
            Matrix4.CreateScale((Vector3)Scale)
            * Matrix4.CreateRotationX(Rotation.X)
            * Matrix4.CreateRotationY(Rotation.Y)
            * Matrix4.CreateRotationZ(Rotation.Z)
            * Matrix4.CreateTranslation((Vector3)Location);

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