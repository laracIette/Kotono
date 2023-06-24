using OpenTK.Mathematics;
using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    internal struct Vertex
    {
        internal Vector Position { get; set; }

        internal Vector Normal { get; set; }

        internal Vector2 TexCoord { get; set; }

        internal Vertex(Vector position, Vector normal, Vector2 texCoord)
        {
            Position = position;
            Normal = normal;
            TexCoord = texCoord;
        }

        internal static readonly int SizeInBytes = sizeof(float) * 8;
    }
}
