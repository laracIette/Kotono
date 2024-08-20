using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Vertex2D(Point position, Point texCoord)
    {
        public Point Position { get; } = position;

        public Point TexCoord { get; } = texCoord;

        public static int SizeInBytes => Point.SizeInBytes * 2;
    }
}
