using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace Kotono.Graphics.Objects
{
    struct Vertex
    {
        public Vector3 position;
        public Vector2 texCoords;
        public Vector3 normal;

        public static readonly int SizeInBytes = Vector3.SizeInBytes * 2 + Vector2.SizeInBytes;
    }
}
