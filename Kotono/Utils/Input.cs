﻿using Kotono.Graphics;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Utils
{
    public static class Input
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

        public static Vector GetMouseRay()
        {
            var mouse = new Rect
            {
                X = MouseState!.Position.X,
                Y = MouseState.Position.Y,
            }.Normalized;

            Matrix4 invProjMat = Matrix4.Invert(KT.ActiveCamera.ProjectionMatrix);
            Matrix4 invViewMat = Matrix4.Invert(KT.ActiveCamera.ViewMatrix);

            Vector4 rayClip = new Vector4(mouse.X, mouse.Y, -1.0f, 1.0f);
            Vector4 rayView = invProjMat * rayClip;
            rayView = new Vector4(rayView.X, rayView.Y, -1.0f, 0.0f);
            Vector4 rayWorld = invViewMat * rayView;
            
            return ((Vector)rayWorld.Xyz).Normalized;
        }
    }
}