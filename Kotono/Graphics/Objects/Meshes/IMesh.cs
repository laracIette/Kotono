using OpenTK.Mathematics;
using System;
using static Kotono.Physics.Fiziks;

namespace Kotono.Graphics.Objects.Meshes
{
    public interface IMesh : IDisposable
    {
        public void Update();

        public void Draw();

        public Vector3 Position { get; }

        public Vector3 Color { get; set; }

        public Vector3 Angle { get; }

        public Vector3 Scale { get; }

        public Matrix4 Model { get; }

        public int VertexArrayObject { get; }

        public int VertexBufferObject { get; }

        public int IndicesCount { get; }

        public int DiffuseMap { get; }

        public int SpecularMap { get; }

        public CollisionState Collision { get; }
    }
}