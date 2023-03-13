using Assimp;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public struct Vertex
    {
        public Vector3D Position { get; set; }
        public Vector2D TexCoord { get; set; }
        public Vector3D Normal { get; set; }

        public Vertex(Vector3D position, Vector2D texCoord, Vector3D normal)
        {
            Position = position;
            TexCoord = texCoord;
            Normal = normal;
        }

        public static readonly int SizeInBytes = sizeof(float) * 8;
    }
}
