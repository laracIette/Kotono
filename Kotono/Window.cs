using Kotono.Graphics;
using Kotono.Graphics.Objects.Lights;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using ShaderType = Kotono.Graphics.ShaderType;

namespace Kotono
{
    public class Window : GameWindow
    {
        private readonly SpotLight _spotLight = new();

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            KT.CreateCamera(new Camera());

            KT.SetCameraAspectRatio(0, (float)Size.X / (float)Size.Y);

            InputManager.Update(KeyboardState, MouseState);
        }

        protected new void Load()
        {
            CursorState = CursorState.Grabbed;
            IsVisible = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            if (!IsFocused) return;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        protected new void RenderFrame()
        {
            if (!IsFocused) return;

            KT.SetShaderFloat(ShaderType.Lighting, "spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.CutOffAngle)));
            KT.SetShaderFloat(ShaderType.Lighting, "spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.OuterCutOffAngle)));

            KT.RenderFrame();

            base.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            InputManager.Update(KeyboardState, MouseState);

            KT.Update();

            if (InputManager.KeyboardState!.IsKeyDown(InputManager.Escape))
            {
                base.Close();
            }

            if (InputManager.KeyboardState.IsKeyPressed(InputManager.Fullscreen))
            {
                WindowState = (WindowState == WindowState.Normal) ?
                    WindowState.Fullscreen :
                    WindowState.Normal;
            }

            if (InputManager.KeyboardState.IsKeyPressed(InputManager.GrabMouse))
            {
                CursorState = (CursorState == CursorState.Normal) ?
                    CursorState.Grabbed :
                    CursorState.Normal;
            }

            _spotLight.Update();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            KT.SetCameraAspectRatio(0, (float)Size.X / (float)Size.Y);
        }

        protected override void OnUnload()
        {
            KT.Exit();

            base.OnUnload();
        }
    }
}
