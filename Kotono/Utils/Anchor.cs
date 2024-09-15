using System;

namespace Kotono.Utils
{
    [Flags]
    public enum Anchor
    {
        Center = 0x0,

        Top = 0x1,
        Bottom = 0x2,
        Left = 0x4,
        Right = 0x8,

        TopLeft = Top | Left,
        TopRight = Top | Right,
        BottomLeft = Bottom | Left,
        BottomRight = Bottom | Right,
    }
}
