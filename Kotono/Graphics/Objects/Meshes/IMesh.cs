using OpenTK.Mathematics;
using System;
using Kotono.Physics;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    public interface IMesh : IDisposable
    {
        public void Update();

        public void Draw();

        public Vector Location { get; }

        public Vector Color { get; set; }

        public Vector Rotation { get; set; }

        public Vector Scale { get; set; }

        public Matrix4 Model { get; }

        public int VertexArrayObject { get; }

        public int VertexBufferObject { get; }

        public int IndicesCount { get; }

        public int DiffuseMap { get; }

        public int SpecularMap { get; }

        public CollisionState CollisionState { get; }

        public bool IsGravity { get; set; }

        public bool IsFiziks { get; set; }

        public Vector Center { get; }

        public Vector[] Vertices { get; }

    }
}