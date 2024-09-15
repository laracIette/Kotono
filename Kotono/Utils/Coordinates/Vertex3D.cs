using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Vertex3D
    {
        public required Vector Pos { get; init; }

        public required Vector Normal { get; init; }

        public required Vector Tangent { get; init; }

        public required Point TexCoords { get; init; }

        public static int SizeInBytes => Vector.SizeInBytes * 3 + Point.SizeInBytes;
    }
}
