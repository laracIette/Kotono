using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Input
{
    public static class Keyboard
    {
        private static KeyboardState? _keyboardState;

        private static KeyboardState KeyboardState
        {
            get => _keyboardState ?? throw new Exception($"error: _keyboardState must not be null");
            set => _keyboardState = value;
        }

        public static void Init(KeyboardState keyboardState)
        {
            KeyboardState = keyboardState;
        }

        public static void Update()
        {
        }

        public static bool IsKeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return KeyboardState.IsKeyPressed(key);
        }
    }
}
