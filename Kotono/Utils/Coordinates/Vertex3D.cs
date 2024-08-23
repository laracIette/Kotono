using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex3D
    {
        public Vector Location { get; set; }

        public Vector Normal { get; set; } 

        public Vector Tangent { get; set; }

        public Point TexCoord { get; set; } 

        public static int SizeInBytes => Vector.SizeInBytes * 3 + Point.SizeInBytes;
    }
}
