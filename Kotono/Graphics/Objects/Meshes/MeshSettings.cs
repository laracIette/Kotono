using Kotono.Graphics.Objects.Shapes;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    public struct MeshSettings
    {
        public int VertexArrayObject;

        public int VertexBufferObject;

        public int IndicesCount;

        public Vector Center;

        public Vector[] Vertices;

        public Triangle[] Triangles;
    }
}
