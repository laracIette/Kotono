using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Sides
    {
        public float Left = 0.0f;

        public float Right = 0.0f;

        public float Top = 0.0f;

        public float Bottom = 0.0f;

        public Sides()
        {
            Left = 0.0f;
            Right = 0.0f;
            Top = 0.0f;
            Bottom = 0.0f;
        }

        public Sides(float left, float right, float top, float bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }
    }
}
