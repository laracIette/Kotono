using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    internal struct Vertex
    {
        internal Vector Location { get; set; }

        internal Vector Normal { get; set; }

        internal Point TexCoord { get; set; }

        internal Vertex(Vector location, Vector normal, Point texCoord)
        {
            Location = location;
            Normal = normal;
            TexCoord = texCoord;
        }

        internal static readonly int SizeInBytes = sizeof(float) * 8;
    }
}
