using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Vertex3D(Vector location, Vector normal, Point texCoord)
    {
        public Vector Location { get; } = location;

        public Vector Normal { get; } = normal;

        public Point TexCoord { get; } = texCoord;

        public static int SizeInBytes => Vector.SizeInBytes * 2 + Point.SizeInBytes;
    }
}
