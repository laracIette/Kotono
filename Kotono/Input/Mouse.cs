using Kotono.Utils;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Input
{
    public enum CursorState
    {
        Normal,
        Centered,
        Confined
    }

    public static partial class Mouse
    {
        internal struct POINT
        {
            internal int X;
            internal int Y;
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


        public static Point Position { get; private set; } = new();

        public static Point PreviousPosition { get; private set; } = new();

        public static Point Delta { get; private set; } = new();

        public static Vector Ray { get; private set; } = Vector.Zero;

        private static MouseState? _mouseState;

        private static MouseState MouseState
        {
            get
            {
                if (_mouseState == null)
                {
                    throw new Exception($"error: _mouseState must not be null");
                }
                else
                {
                    return _mouseState;
                }
            }
            set
            {
                _mouseState = value;
            }
        }

        public static Vector2 ScrollDelta => MouseState.ScrollDelta;

        public static CursorState CursorState { get; set; } = CursorState.Centered;

        public static void Init(MouseState mouseState)
        {
            MouseState = mouseState;
        }

        public static void Update()
        {
            PreviousPosition = Position;
            Position = GetCursorPos();

            if (CursorState == CursorState.Confined)
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

            if (Delta != Point.Zero)
            {
                UpdateRay();
            }

            if (CursorState == CursorState.Centered)
            {
                var center = new Point(KT.Dest.X + KT.Dest.W / 2, KT.Dest.Y + KT.Dest.H / 2);
                if (Position != center)
                {
                    SetCursorPos(center);
                    Position = center;
                }
            }
        }

        private static void UpdateRay()
        {
            var mouse = (Position - KT.Position).WorldSpace;

            Vector4 rayClip = new Vector4(mouse.X, mouse.Y, -1.0f, 1.0f);
            Vector4 rayView = Matrix4.Invert(KT.ActiveCamera.ProjectionMatrix) * rayClip;
            rayView.Z = -1.0f; rayView.W = 0.0f;
            Vector4 rayWorld = KT.ActiveCamera.ViewMatrix * rayView;

            Ray = ((Vector)rayWorld.Xyz).Normalized;
        }

        public static bool IsButtonDown(MouseButton button)
        {
            return MouseState.IsButtonDown(button);
        }

        public static bool IsButtonPressed(MouseButton button)
        {
            return MouseState.IsButtonPressed(button);
        }

        public static bool IsButtonReleased(MouseButton button)
        {
            return MouseState.IsButtonReleased(button);
        }
    }
}
