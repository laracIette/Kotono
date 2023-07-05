using Kotono.Graphics;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Utils
{
    public static class Input
    {
        public static Keys Escape { get; set; } = Keys.Escape;

        public static Keys Fullscreen { get; set; } = Keys.F11;

        public static Keys GrabMouse { get; set; } = Keys.Enter;

        public static CursorState CursorState { get; set; } = CursorState.Normal;

        public static KeyboardState? KeyboardState { get; private set; }

        public static MouseState? MouseState { get; private set; }

        public static void Update(KeyboardState keyboardState, MouseState mouseState)
        {
            KeyboardState = keyboardState;
            MouseState = mouseState;
        }

        public static Vector GetMouseRay()
        {
            var mouse = ((Point)MouseState!.Position).Normalized;

            Vector4 rayClip = new Vector4(mouse.X, mouse.Y, -1.0f, 1.0f);
            Vector4 rayView = Matrix4.Invert(KT.ActiveCamera.ProjectionMatrix) * rayClip;
            rayView.Z = -1.0f; rayView.W = 0.0f;
            Vector4 rayWorld = KT.ActiveCamera.ViewMatrix * rayView;
            
            return ((Vector)rayWorld.Xyz).Normalized;
        }
    }
}
