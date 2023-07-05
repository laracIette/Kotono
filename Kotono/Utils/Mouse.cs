using Kotono.Graphics.Objects;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Utils
{
    public static partial class Mouse
    {
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetCursorPos(int x, int y);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool GetCursorPos(out POINT pos);

        private static Point GetCursorPos()
        {
            if (GetCursorPos(out POINT pos))
            {
                return new Point(pos.X, pos.Y);
            }
            else
            {
                throw new Exception("error: couldn't retrieve cursor position");
            }
        }

        private static void SetCursorPos(Point pos)
        {
            SetCursorPos((int)pos.X, (int)pos.Y);
        }

        private static Point _position = new();

        public static Point Position
        {
            get => _position;
            private set => _position = value;
        }

        private static Point _previousPosition = new();

        public static Point PreviousPosition
        {
            get => _previousPosition;
            private set => _previousPosition = value;
        }

        private static Point _delta = new();

        public static Point Delta
        {
            get => _delta;
            private set => _delta = value;
        }

        public static void Update()
        {
            PreviousPosition = Position;
            Position = GetCursorPos();

            if (Input.MouseState!.IsButtonDown(MouseButton.Left))
            {
                var delta = Point.Zero;
                if (Position.X < KT.Dest.X)
                {
                    delta.X += KT.Dest.W;
                }
                else if (Position.X > (KT.Dest.X + KT.Dest.W))
                {
                    delta.X -= KT.Dest.W;
                }
                if (Position.Y < KT.Dest.Y)
                {
                    delta.Y += KT.Dest.H;
                }
                else if (Position.Y > (KT.Dest.Y + KT.Dest.H))
                {
                    delta.Y -= KT.Dest.H;
                }
                if (delta != Point.Zero)
                {
                    PreviousPosition += delta;
                    Position += delta;
                    SetCursorPos(Position);
                }
            }

            Delta = Position - PreviousPosition;
        }
    }
}
