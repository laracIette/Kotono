using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Input
{
    public static class Keyboard
    {
        private static KeyboardState? _keyboardState;

        internal static KeyboardState KeyboardState
        {
            get => _keyboardState ?? throw new Exception($"error: _keyboardState must not be null");
            set => _keyboardState = value;
        }

        public static void Init()
        {
        }

        public static void Update()
        {
        }

        public static bool IsKeyDown(Keys key) => KeyboardState.IsKeyDown(key);

        public static bool IsKeyPressed(Keys key) => KeyboardState.IsKeyPressed(key);
    }
}
