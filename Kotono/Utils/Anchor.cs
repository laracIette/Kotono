using System;

namespace Kotono.Utils
{
    [Flags]
    public enum Anchor
    {
        Center = 0b0000,

        Top = 0b0001,
        Bottom = 0b0010,
        Left = 0b0100,
        Right = 0b1000,

        TopLeft = Top | Left,
        TopRight = Top | Right,
        BottomLeft = Bottom | Left,
        BottomRight = Bottom | Right,
    }
}
