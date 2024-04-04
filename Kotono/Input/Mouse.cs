using Kotono.Engine;
using Kotono.Graphics.Objects;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Runtime.CompilerServices;
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

        internal static Point Position => PositionFromOrigin - Window.Position;

        internal static Point PreviousPosition => PreviousPositionFromOrigin - Window.Position;

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
            if (StateManager.EngineState == EngineState.Play)
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
                    delta.X += Window.Dest.W;
                }
                else if (Position.X > Window.Dest.W)
                {
                    delta.X -= Window.Dest.W;
                }
                if (Position.Y < 0.0f)
                {
                    delta.Y += Window.Dest.H;
                }
                else if (Position.Y > Window.Dest.H)
                {
                    delta.Y -= Window.Dest.H;
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
                var center = Window.Dest.TopRight;
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
            Vector4 rayView = Matrix4.Invert(ObjectManager.ActiveCamera.ProjectionMatrix) * rayClip;
            rayView.Z = -1.0f; rayView.W = 0.0f;
            Vector4 rayWorld = ObjectManager.ActiveCamera.ViewMatrix * rayView;

            Ray = ((Vector)rayWorld.Xyz).Normalized;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsButtonDown(MouseButton button) => MouseState.IsButtonDown(button);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool WasButtonDown(MouseButton button) => MouseState.WasButtonDown(button);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsButtonPressed(MouseButton button) => MouseState.IsButtonPressed(button);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsButtonReleased(MouseButton button) => MouseState.IsButtonReleased(button);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ShowCursor() => ShowCursor(true);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void HideCursor() => ShowCursor(false);
    }
}
