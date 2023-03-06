using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public interface IMesh
    {
        public void Update(float deltaTime, IEnumerable<IMesh> models);

        public Vector3 Position { get; }

        public Matrix4 Model { get; }

        public int VertexArrayObject { get; }

        public int VertexBufferObject { get; }

        public int VerticesCount { get; }

        public int DiffuseMap { get; }

        public int SpecularMap { get; }
    }
}