using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Sides
    {
        public readonly float Left;

        public readonly float Right;

        public readonly float Top;

        public readonly float Bottom;

        public Sides(float left, float right, float top, float bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public Sides() : this(0.0f, 0.0f, 0.0f, 0.0f) { }
    }
}
