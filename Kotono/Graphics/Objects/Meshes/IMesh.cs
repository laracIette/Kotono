using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Meshes
{
    public interface IMesh
    {
        public void Update();

        public void Draw();

        public Vector3 Position { get; }

        public Vector3 Color { get; }

        public Vector3 Angle { get; }

        public Vector3 Scale { get; }

        public Matrix4 Model { get; }

        public int VertexArrayObject { get; }

        public int VertexBufferObject { get; }

        public int IndicesCount { get; }

        public int DiffuseMap { get; }

        public int SpecularMap { get; }
    }
}