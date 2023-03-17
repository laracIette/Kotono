using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Utils
{
    public static class InputManager
    {
        public static Keys Escape { get; set; } = Keys.Escape;

        public static Keys Fullscreen { get; set; } = Keys.F11;

        public static Keys GrabMouse { get; set; } = Keys.Enter;

        public static KeyboardState? KeyboardState { get; private set; }

        public static MouseState? MouseState { get; private set; }

        public static void Update(KeyboardState keyboardState, MouseState mouseState)
        {
            KeyboardState = keyboardState;
            MouseState = mouseState;
        }
    }
}
