using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Utils
{
    public static class InputManager
    {
        public static KeyboardState KeyboardState;

        public static MouseState MouseState;

        public static Keys Escape = Keys.Escape;

        public static Keys Fullscreen = Keys.F11;

        public static Keys GrabMouse = Keys.Enter;

        public static void Update(KeyboardState keyboardState, MouseState mouseState)
        {
            KeyboardState = keyboardState;
            MouseState = mouseState;
        }
    }
}
