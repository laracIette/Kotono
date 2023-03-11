using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Meshes
{
    public interface IMesh
    {
        public void Update();

        public void Draw();

        public Vector3 Position { get; }

        public Matrix4 Model { get; }

        public int VertexArrayObject { get; }

        public int VertexBufferObject { get; }

        public int VerticesCount { get; }

        public int DiffuseMap { get; }

        public int SpecularMap { get; }
    }
}