﻿using Kotono.Engine;
using Kotono.Graphics.Objects;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kotono.Input
{
    internal static partial class Mouse
    {
        private record class Method(InputAction InputAction, IObject Instance, MethodInfo MethodInfo);

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

        private static Point RawCursorPos
        {
            get
            {
                Point result;

                if (GetCursorPos(out POINT pos))
                {
                    result = new Point(pos.X, pos.Y);
                }
                else
                {
                    result = Point.Zero;
                    Logger.Log("error: couldn't retrieve cursor position");
                }

                return result;
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

        internal static Point PositionFromOrigin { get; private set; } = Point.Zero;

        internal static Point PreviousPositionFromOrigin { get; private set; } = Point.Zero;

        internal static Point Delta { get; private set; } = Point.Zero;

        internal static Point Position => PositionFromOrigin - Window.Position;

        internal static Point PreviousPosition => PreviousPositionFromOrigin - Window.Position;

        internal static Vector Ray { get; private set; } = Vector.Zero;

        internal static Point ScrollDelta => (Point)MouseState.ScrollDelta;

        internal static CursorState CursorState { get; set; } = CursorState.Normal;

        private static readonly Dictionary<MouseButton, List<Method>> _buttonActions = []; // maybe array

        static Mouse()
        {
            foreach (var button in Enum.GetValues<MouseButton>().Distinct())
            {
                _buttonActions[button] = [];
            }
        }

        internal static void Update()
        {
            if (StateManager.EngineState == EngineState.Play)
            {
                CursorState = CursorState.Centered;
            }

            PreviousPositionFromOrigin = PositionFromOrigin;
            PositionFromOrigin = RawCursorPos;

            if (CursorState == CursorState.Confined)
            {
                var delta = Point.Zero;
                if (Position.X < 0.0f)
                {
                    delta.X += Window.Size.X;
                }
                else if (Position.X > Window.Size.X)
                {
                    delta.X -= Window.Size.X;
                }
                if (Position.Y < 0.0f)
                {
                    delta.Y += Window.Size.Y;
                }
                else if (Position.Y > Window.Size.Y)
                {
                    delta.Y -= Window.Size.Y;
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
                //var center = Window.Rect.TopRight; TODO
                var center = new Point(960.0f, 540.0f);
                if (PositionFromOrigin != center)
                {
                    SetCursorPos(center);
                    PositionFromOrigin = center;
                }
            }

            for (int i = 0; i < _buttonActions.Count; i++)
            {
                var (button, methods) = _buttonActions.ElementAt(i);

                bool isButtonPressed = IsButtonPressed(button);
                bool isButtonDown = IsButtonDown(button);
                bool isButtonReleased = IsButtonReleased(button);

                for (int j = methods.Count - 1; j >= 0; j--)
                {
                    var method = methods[j];

                    if ((isButtonPressed && method.InputAction == InputAction.Pressed)
                     || (isButtonDown && method.InputAction == InputAction.Down)
                     || (isButtonReleased && method.InputAction == InputAction.Released))
                    {
                        method.MethodInfo.Invoke(method.Instance, null);
                    }
                }
            }
        }

        private static void UpdateRay()
        {
            var mouse = Position.NDC;

            var rayClip = new Vector4(mouse.X, mouse.Y, -1.0f, 1.0f);
            var rayView = Matrix4.Invert(Camera.Active.ProjectionMatrix) * rayClip;
            rayView.Z = -1.0f; rayView.W = 0.0f;
            var rayWorld = Camera.Active.ViewMatrix * rayView;

            Ray = ((Vector)rayWorld.Xyz).Normalized;
        }

        /// <summary>
        /// Subscribe a method to a keyboard key <see cref="InputAction"/>.
        /// </summary>
        /// <param name="instance"> The object the method belongs to. </param>
        /// <param name="methodInfo"> The method to subscribe. </param>
        internal static void Subscribe(IObject instance, MethodInfo methodInfo)
        {
            InputAction action;
            int nameEnd;

            if (methodInfo.Name.EndsWith("Pressed"))
            {
                action = InputAction.Pressed;
                nameEnd = 13;
            }
            else if (methodInfo.Name.EndsWith("Down"))
            {
                action = InputAction.Down;
                nameEnd = 10;
            }
            else if (methodInfo.Name.EndsWith("Released"))
            {
                action = InputAction.Released;
                nameEnd = 14;
            }
            else
            {
                throw new KotonoException($"couldn't parse method \"{methodInfo.Name}\" to Action");
            }

            if (Enum.TryParse(methodInfo.Name[2..^nameEnd], out MouseButton button))
            {
                _buttonActions[button].Add(new Method(action, instance, methodInfo));
            }
            else
            {
                Logger.Log($"error: couldn't parse \"{methodInfo.Name[2..^10]}\" to Keys in Keyboard.Subscribe(IObject, MethodInfo).");
            }
        }

        /// <summary>
        /// Unsubscribe all the methods of an object from keyboard key <see cref="InputAction"/>s.
        /// </summary>
        /// <param name="instance"> The object which to unsubscribe the methods. </param>
        internal static void Unsubscribe(IObject instance)
        {
            foreach (var methods in _buttonActions.Values)
            {
                methods.RemoveAll(m => m.Instance == instance);
            }
        }


        /// <inheritdoc cref="MouseState.IsButtonDown(MouseButton)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsButtonDown(MouseButton button) => MouseState.IsButtonDown(button);

        /// <inheritdoc cref="MouseState.WasButtonDown(MouseButton)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool WasButtonDown(MouseButton button) => MouseState.WasButtonDown(button);

        /// <inheritdoc cref="MouseState.IsButtonPressed(MouseButton)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsButtonPressed(MouseButton button) => MouseState.IsButtonPressed(button);

        /// <inheritdoc cref="MouseState.IsButtonReleased(MouseButton)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsButtonReleased(MouseButton button) => MouseState.IsButtonReleased(button);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ShowCursor() => ShowCursor(true);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void HideCursor() => ShowCursor(false);
    }
}
