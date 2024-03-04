using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Input
{
    internal static class Keyboard
    {
        private static KeyboardState? _keyboardState;

        internal static KeyboardState KeyboardState
        {
            get => _keyboardState ?? throw new Exception($"error: _keyboardState must not be null");
            set => _keyboardState = value;
        }

        internal static bool IsKeyDown(Keys key) => KeyboardState.IsKeyDown(key);

        internal static bool IsKeyPressed(Keys key) => KeyboardState.IsKeyPressed(key);
    }
}
