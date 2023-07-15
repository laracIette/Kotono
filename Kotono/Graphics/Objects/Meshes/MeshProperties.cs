using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    public struct MeshProperties
    {
        public int VertexArrayObject;
        
        public int VertexBufferObject;

        public int IndicesCount;

        public Vector Center;

        public Vector[] Vertices;

        public Triangle[] Triangles;
    }
}
