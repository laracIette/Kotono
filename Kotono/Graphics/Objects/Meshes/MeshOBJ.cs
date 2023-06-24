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
    public class MeshOBJ : IMesh
    {
        private static readonly Dictionary<string, Tuple<int[], Vector, Vector[]>> _paths = new();

        private Vector _position;

        private Vector _positionVelocity;

        private Vector _angleVelocity;

        private readonly IHitbox[] _hitboxes;

        protected readonly ShaderType _shaderType;

        public MeshOBJ(string path, Vector position, Vector rotation, Vector scale, string diffusePath, string specularPath, ShaderType shaderType, Vector color, IHitbox[] hitboxes)
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
                            var pos = new Vector(mesh.Vertices[j].X, mesh.Vertices[j].Y, mesh.Vertices[j].Z);
                            var normal = new Vector(mesh.Normals[j].X, mesh.Normals[j].Y, mesh.Normals[j].Z);
                            var texCoord = new Vector2(mesh.TextureCoordinateChannels[0][j].X, mesh.TextureCoordinateChannels[0][j].Y);

                            tempVertices.Add(new Vertex(pos, normal, texCoord));
                        }

                        models[i] = tempVertices;
                        indices[i] = mesh.GetIndices().ToList();
                    }
                }

                var center = Vector.Zero;
                models[0].ForEach(v => center += v.Position);
                center /= models[0].Count;

                var vertices = new Vector[models[0].Count];
                for (int i = 0; i < models[0].Count; i++)
                {
                    vertices[i] = models[0][i].Position;
                }

                // create vertex array
                int vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayObject);

                // create vertex buffer
                int vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, models[0].Count * Vertex.SizeInBytes, models[0].ToArray(), BufferUsageHint.StaticDraw);

                int positionAttributeLocation = KT.GetShaderAttribLocation(shaderType, "aPos");
                GL.EnableVertexAttribArray(positionAttributeLocation);
                GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

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
                    vertices
                );
            }

            VertexArrayObject = _paths[path].Item1[0];
            VertexBufferObject = _paths[path].Item1[1];
            IndicesCount = _paths[path].Item1[2];
            Center = _paths[path].Item2;
            Vertices = _paths[path].Item3;
            Position = position;
            Rotation = rotation;
            Scale = scale;
            DiffuseMap = diffuseMap;
            SpecularMap = specularMap;
            _shaderType = shaderType;
            Color = color;

            _hitboxes = hitboxes;

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Position = Position;
                hitbox.Rotation = Vector.Zero;
                hitbox.Scale = Scale * 2;
                hitbox.Color = Vector.UnitX;
            }
        }

        public void Update()
        {
            var tempPos = Position;

            //AngleVelocity += Random.Vector(-0.1f, 0.1f);
            //Rotation += AngleVelocity * Time.DeltaS;

            //PositionVelocity += Random.Vector(-0.1f, 0.1f);
            //tempPos += PositionVelocity * Time.DeltaS;

            if (IsGravity)
            {
                tempPos += Fiziks.Gravity * Time.DeltaS;
            }

            if (IsFiziks)
            {
                Fiziks.Update(this);
            }

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Position = tempPos;

                if ((Collision == CollisionState.BlockAll) && hitbox.IsColliding())
                {
                    hitbox.Position = Position;
                }
                else
                {
                    Position = tempPos;
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

        public CollisionState Collision { get; set; }

        public int VertexArrayObject { get; }

        public int VertexBufferObject { get; }

        public int IndicesCount { get; }

        public int DiffuseMap { get; }

        public int SpecularMap { get; }

        public Vector Color { get; set; }

        public Vector Position
        {
            get => _position;
            set
            {
                _position.X = MathHelper.Clamp(value.X, -20.0f, 20.0f);
                _position.Y = MathHelper.Clamp(value.Y, -20.0f, 20.0f);
                _position.Z = MathHelper.Clamp(value.Z, -20.0f, 20.0f);
            }
        }

        private Vector PositionVelocity
        {
            get => _positionVelocity;
            set
            {
                _positionVelocity.X = MathHelper.Clamp(value.X, -1.0f, 1.0f);
                _positionVelocity.Y = MathHelper.Clamp(value.Y, -1.0f, 1.0f);
                _positionVelocity.Z = MathHelper.Clamp(value.Z, -1.0f, 1.0f);
            }
        }

        public Vector Rotation { get; set; }

        public Vector Scale { get; set; }

        private Vector AngleVelocity
        {
            get => _angleVelocity;
            set
            {
                _angleVelocity.X = MathHelper.Clamp(value.X, -2.5f, 2.5f);
                _angleVelocity.Y = MathHelper.Clamp(value.Y, -2.5f, 2.5f);
                _angleVelocity.Z = MathHelper.Clamp(value.Z, -2.5f, 2.5f);
            }
        }

        public Matrix4 Model =>
            Matrix4.CreateScale((Vector3)Scale)
            * Matrix4.CreateRotationX(Rotation.X)
            * Matrix4.CreateRotationY(Rotation.Y)
            * Matrix4.CreateRotationZ(Rotation.Z)
            * Matrix4.CreateTranslation((Vector3)Position);

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