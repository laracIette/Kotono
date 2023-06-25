using OpenTK.Mathematics;
using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    internal struct Vertex
    {
        internal Vector Location { get; set; }

        internal Vector Normal { get; set; }

        internal Vector2 TexCoord { get; set; }

        internal Vertex(Vector location, Vector normal, Vector2 texCoord)
        {
            Location = location;
            Normal = normal;
            TexCoord = texCoord;
        }

        internal static readonly int SizeInBytes = sizeof(float) * 8;
    }
}
