using Kotono.Engine;
using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
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

        private static MouseState? _mouseState;

        internal static MouseState MouseState
        {
            get => _mouseState ?? throw new Exception($"error: _mouseState must not be null");
            set => _mouseState = value;
        }

        private static readonly Dictionary<MouseButton, EventHandler<TimedEventArgs>?> _buttonsPressed = [];

        internal static Point PositionFromOrigin { get; private set; } = Point.Zero;

        internal static Point PreviousPositionFromOrigin { get; private set; } = Point.Zero;

        internal static Point Delta { get; private set; } = Point.Zero;

        internal static Point Position => PositionFromOrigin - Window.Position;

        internal static Point PreviousPosition => PreviousPositionFromOrigin - Window.Position;

        internal static Vector Ray { get; private set; } = Vector.Zero;

        internal static Point ScrollDelta => (Point)MouseState.ScrollDelta;

        internal static CursorState CursorState { get; set; } = CursorState.Normal;

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
                    delta.X += Window.Rect.Size.X;
                }
                else if (Position.X > Window.Rect.Size.X)
                {
                    delta.X -= Window.Rect.Size.X;
                }
                if (Position.Y < 0.0f)
                {
                    delta.Y += Window.Rect.Size.Y;
                }
                else if (Position.Y > Window.Rect.Size.Y)
                {
                    delta.Y -= Window.Rect.Size.Y;
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
                var center = Window.Rect.TopRight;
                if (PositionFromOrigin != center)
                {
                    SetCursorPos(center);
                    PositionFromOrigin = center;
                }
            }

            foreach (var button in _buttonsPressed.Keys)
            {
                if (IsButtonPressed(button))
                {
                    _buttonsPressed[button]?.Invoke(null, new TimedEventArgs());
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

        internal static void SubscribeButtonPressed(EventHandler<TimedEventArgs> func, MouseButton button)
        {
            if (!_buttonsPressed.ContainsKey(button))
            {
                _buttonsPressed[button] = null;
            }

            _buttonsPressed[button] += func;
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
