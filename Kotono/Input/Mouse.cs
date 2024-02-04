using Kotono.Engine;
using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Input
{
    internal static partial class Mouse
    {
        internal struct POINT
        {
            internal int X;
            internal int Y;
        }

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool ShowCursor([MarshalAs(UnmanagedType.Bool)] bool bShow);

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

        internal static Point PositionFromOrigin { get; private set; } = Point.Zero;

        internal static Point PreviousPositionFromOrigin { get; private set; } = Point.Zero;

        internal static Point Delta { get; private set; } = Point.Zero;

        internal static Point Position => PositionFromOrigin - KT.Position;

        internal static Point PreviousPosition => PreviousPositionFromOrigin - KT.Position;

        internal static Vector Ray { get; private set; } = Vector.Zero;

        private static MouseState? _mouseState;

        internal static MouseState MouseState
        {
            get => _mouseState ?? throw new Exception($"error: _mouseState must not be null");
            set => _mouseState = value;
        }

        internal static Point ScrollDelta => (Point)MouseState.ScrollDelta;

        internal static CursorState CursorState { get; set; } = CursorState.Centered;

        internal static void Update()
        {
            if (KT.UserMode == UserMode.Play)
            {
                CursorState = CursorState.Centered;
            }

            PreviousPositionFromOrigin = PositionFromOrigin;
            PositionFromOrigin = GetCursorPos();

            if (CursorState == CursorState.Confined)
            {
                var delta = Point.Zero;
                if (Position.X < 0.0f)
                {
                    delta.X += KT.Dest.W;
                }
                else if (Position.X > KT.Dest.W)
                {
                    delta.X -= KT.Dest.W;
                }
                if (Position.Y < 0.0f)
                {
                    delta.Y += KT.Dest.H;
                }
                else if (Position.Y > KT.Dest.H)
                {
                    delta.Y -= KT.Dest.H;
                }

                if (delta != Point.Zero)
                {
                    PreviousPositionFromOrigin += delta;
                    PositionFromOrigin += delta;
                    SetCursorPos(PositionFromOrigin);
                }
            }

            Delta = PositionFromOrigin - PreviousPositionFromOrigin;

            if (Delta != Point.Zero)
            {
                UpdateRay();
            }

            if (CursorState == CursorState.Centered)
            {
                var center = KT.Dest.TopRight;
                if (PositionFromOrigin != center)
                {
                    SetCursorPos(center);
                    PositionFromOrigin = center;
                }
            }
        }

        private static void UpdateRay()
        {
            var mouse = Position.NDC;

            Vector4 rayClip = new(mouse.X, mouse.Y, -1.0f, 1.0f);
            Vector4 rayView = Matrix4.Invert(CameraManager.ActiveCamera.ProjectionMatrix) * rayClip;
            rayView.Z = -1.0f; rayView.W = 0.0f;
            Vector4 rayWorld = CameraManager.ActiveCamera.ViewMatrix * rayView;

            Ray = ((Vector)rayWorld.Xyz).Normalized;
        }

        internal static bool IsButtonDown(MouseButton button) => MouseState.IsButtonDown(button);

        internal static bool WasButtonDown(MouseButton button) => MouseState.WasButtonDown(button);

        internal static bool IsButtonPressed(MouseButton button) => MouseState.IsButtonPressed(button);

        internal static bool IsButtonReleased(MouseButton button) => MouseState.IsButtonReleased(button);

        internal static void ShowCursor()
        {
            ShowCursor(true);
        }

        internal static void HideCursor()
        {
            ShowCursor(false);
        }
    }
}
