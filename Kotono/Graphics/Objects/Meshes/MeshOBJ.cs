﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using Kotono.Utils;
using Random = Kotono.Utils.Random;
using System;
using Assimp;
using System.IO;
using Path = Kotono.Utils.Path;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Kotono.Graphics.Objects.Meshes
{
    public class MeshOBJ : IMesh, IDisposable
    {
        private Vector3 _position;

        private Vector3 _positionVelocity;

        private Vector3 _angleVelocity;

        private readonly Shader _shader;

        public MeshOBJ(string path, Vector3 position, Vector3 angle, Vector3 scale, string diffusePath, string specularPath, Shader shader, Vector3 color)
        {
            var diffuseMap = TextureManager.LoadTexture(diffusePath);
            var specularMap = TextureManager.LoadTexture(specularPath);

            if (!ObjectManager.Paths.ContainsKey(path))
            {
                List<Vertex>[] models;
                List<int>[] indices;

                using (var importer = new AssimpContext())
                {
                    var scene = importer.ImportFile(Path.Assets + path, PostProcessSteps.Triangulate);

                    models = new List<Vertex>[scene.Meshes.Count];
                    indices = new List<int>[scene.Meshes.Count];
                    for (int i = 0; i < scene.Meshes.Count; i++)
                    {
                        var mesh = scene.Meshes[i];
                        var vertices = new List<Vertex>();

                        for (int j = 0; j < mesh.Vertices.Count; j++)
                        {
                            var pos = new Vector3(mesh.Vertices[j].X, mesh.Vertices[j].Y, mesh.Vertices[j].Z);
                            var normal = new Vector3(mesh.Normals[j].X, mesh.Normals[j].Y, mesh.Normals[j].Z);
                            var texCoord = new Vector2(mesh.TextureCoordinateChannels[0][j].X, mesh.TextureCoordinateChannels[0][j].Y);

                            vertices.Add(new Vertex(pos, normal, texCoord));
                        }

                        models[i] = vertices;
                        indices[i] = mesh.GetIndices().ToList();
                    }
                }

                // create vertex array
                int vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayObject);

                // create vertex buffer
                int vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, models[0].Count * Vertex.SizeInBytes, models[0].ToArray(), BufferUsageHint.DynamicDraw);

                int positionAttributeLocation = shader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionAttributeLocation);
                GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

                int normalAttributeLocation = shader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalAttributeLocation);
                GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 3);

                int texCoordAttributeLocation = shader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordAttributeLocation);
                GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 6);

                // create element buffer
                int elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices[0].Count * sizeof(int), indices[0].ToArray(), BufferUsageHint.DynamicDraw);

                ObjectManager.Paths[path] = Tuple.Create(vertexArrayObject, vertexBufferObject, indices[0].Count);
            }

            VertexArrayObject = ObjectManager.Paths[path].Item1;
            VertexBufferObject = ObjectManager.Paths[path].Item2;
            IndicesCount = ObjectManager.Paths[path].Item3;
            Position = position;
            Angle = angle;
            Scale = scale;
            DiffuseMap = diffuseMap;
            SpecularMap = specularMap;
            _shader = shader;
            Color = color;
        }

        public void Update()
        {
            AngleVelocity += Random.Vector3(-0.1f, 0.1f);
            Angle += AngleVelocity * Time.Delta;

            PositionVelocity += Random.Vector3(-0.1f, 0.1f);

            bool collides = false;

            foreach (var mesh in ObjectManager.Meshes.Where(m => this != m))
            {
                if (Vector3.Distance(Position + PositionVelocity * Time.Delta, mesh.Position) <= 1.5f)
                {
                    collides = true;
                    break;
                }
            }

            if (!collides)
            {
                Position += PositionVelocity * Time.Delta;
            }
        }

        public virtual void Draw()
        {
            TextureManager.UseTexture(DiffuseMap, TextureUnit.Texture0);
            TextureManager.UseTexture(SpecularMap, TextureUnit.Texture1);

            _shader.SetMatrix4("model", Model);
            _shader.SetVector3("color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        public int VertexArrayObject { get; }

        public int VertexBufferObject { get; }

        public int IndicesCount { get; }

        public int DiffuseMap { get; }

        public int SpecularMap { get; }

        public Vector3 Color { get; set; }

        public Vector3 Position
        {
            get => _position;
            private set
            {
                _position.X = MathHelper.Clamp(value.X, -20.0f, 20.0f);
                _position.Y = MathHelper.Clamp(value.Y, -20.0f, 20.0f);
                _position.Z = MathHelper.Clamp(value.Z, -20.0f, 20.0f);
            }
        }
        private Vector3 PositionVelocity
        {
            get => _positionVelocity;
            set
            {
                _positionVelocity.X = MathHelper.Clamp(value.X, -1.0f, 1.0f);
                _positionVelocity.Y = MathHelper.Clamp(value.Y, -1.0f, 1.0f);
                _positionVelocity.Z = MathHelper.Clamp(value.Z, -1.0f, 1.0f);
            }
        }

        private Vector3 Angle { get; set; }

        private Vector3 Scale { get; set; }

        private Vector3 AngleVelocity
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
            Matrix4.CreateScale(Scale)
            * Matrix4.CreateRotationX(Angle.X)
            * Matrix4.CreateRotationY(Angle.Y)
            * Matrix4.CreateRotationZ(Angle.Z)
            * Matrix4.CreateTranslation(Position);

        public void Dispose()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.DeleteVertexArray(VertexArrayObject);
            GL.DeleteVertexArray(VertexBufferObject);
        }
    }
}