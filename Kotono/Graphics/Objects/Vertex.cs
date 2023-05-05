using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    internal struct Vertex
    {
        internal Vector3 Position { get; set; }

        internal Vector3 Normal { get; set; }

        internal Vector2 TexCoord { get; set; }

        internal Vertex(Vector3 position, Vector3 normal, Vector2 texCoord)
        {
            Position = position;
            Normal = normal;
            TexCoord = texCoord;
        }

        internal static readonly int SizeInBytes = sizeof(float) * 8;
    }
}
