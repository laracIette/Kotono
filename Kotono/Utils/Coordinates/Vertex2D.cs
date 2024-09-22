using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Vertex2D
    {
        public required Point Pos { get; init; }

        public required Point TexCoords { get; init; }

        public static int SizeInBytes => Point.SizeInBytes * 2;
    }
}
