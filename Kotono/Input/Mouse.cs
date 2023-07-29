using Kotono.Engine;
using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Input
{
    public static partial class Mouse
    {
        public struct POINT
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


        public static Point Position { get; private set; } = new();

        public static Point PreviousPosition { get; private set; } = new();

        public static Point Delta { get; private set; } = new();

        public static Point RelativePosition => Position - KT.Position;

        public static Vector Ray { get; private set; } = Vector.Zero;

        private static MouseState? _mouseState;

        private static MouseState MouseState
        {
            get => _mouseState ?? throw new Exception($"error: _mouseState must not be null");
            set => _mouseState = value;
        }

        public static Point ScrollDelta => (Point)MouseState.ScrollDelta;

        public static CursorState CursorState { get; set; } = CursorState.Centered;

        public static void Init(MouseState mouseState)
        {
            MouseState = mouseState;
        }

        public static void Update()
        {
            if (KT.UserMode == UserMode.Play)
            {
                CursorState = CursorState.Centered;
            }

            PreviousPosition = Position;
            Position = GetCursorPos();

            if (CursorState == CursorState.Confined)
            {
                var delta = Point.Zero;
                if (RelativePosition.X < 0)
                {
                    delta.X += KT.Dest.W;
                }
                else if (RelativePosition.X > KT.Dest.W)
                {
                    delta.X -= KT.Dest.W;
                }
                if (RelativePosition.Y < 0)
                {
                    delta.Y += KT.Dest.H;
                }
                else if (RelativePosition.Y > KT.Dest.H)
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
            Vector4 rayView = Matrix4.Invert(CameraManager.ActiveCamera.ProjectionMatrix) * rayClip;
            rayView.Z = -1.0f; rayView.W = 0.0f;
            Vector4 rayWorld = CameraManager.ActiveCamera.ViewMatrix * rayView;

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
