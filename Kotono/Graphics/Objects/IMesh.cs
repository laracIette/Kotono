using Kotono.Graphics.Objects.Meshes;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public interface IMesh
    {
        public void Update(float deltaTime, IEnumerable<IMesh> models);

        public Vector3 Position { get; }

        public Matrix4 ModelMatrix { get; }

        public int VertexArrayObject { get; }

        public int IndexBufferObject { get; }

        public int IndexCount { get; }
    }
}
