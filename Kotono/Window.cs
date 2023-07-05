using Kotono.Graphics;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono
{
    public class Window : GameWindow
    {
        private double _stalledTime = 0;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string kotonoPath, string projectPath, int maxFrameRate)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            KT.KotonoPath = kotonoPath;
            KT.ProjectPath = projectPath;
            KT.MaxFrameRate = maxFrameRate;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            KT.CreateCamera(new Camera());

            KT.SetWindowPosition((Point)Location);
            KT.SetWindowSize((Point)Size);

            KT.Init();

            Mouse.Init(MouseState);
            Keyboard.Init(KeyboardState);
        }

        protected new void Load()
        {
            CursorState = CursorState.Normal;
            Mouse.CursorState = CursorState;
            IsVisible = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // don't render frame if current FrameRate > desired FrameRate
            if (!IsFocused || (KT.GetFrameRate() > KT.MaxFrameRate - 1))
            {
                _stalledTime += e.Time;
                KT.AddFrameTime(_stalledTime);
                return;
            }

            _stalledTime = 0;

            KT.AddFrameTime(e.Time);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            KT.RenderFrame();

            base.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KT.AddUpdateTime(e.Time);

            Mouse.Update();
            Keyboard.Update();

            KT.Update();

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                base.Close();
            }

            if (Keyboard.IsKeyPressed(Keys.F11))
            {
                WindowState = (WindowState == WindowState.Normal) ?
                    WindowState.Fullscreen :
                    WindowState.Normal;
            }

            if (Keyboard.IsKeyPressed(Keys.Enter))
            {
                //CursorState = (CursorState == CursorState.Normal) ?
                    //CursorState.Grabbed :
                    //CursorState.Normal;

                //TODO: do we rly need to update
                Mouse.CursorState = CursorState;
            }

            if (Keyboard.IsKeyPressed(Keys.S) && Keyboard.IsKeyDown(Keys.LeftControl))
            {
                KT.Save();
                KT.Print("saved", Color.FromHex("#88FF10"));
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            KT.SetWindowPosition((Point)Location);
            KT.SetWindowSize((Point)Size);
        }

        protected override void OnMove(WindowPositionEventArgs e)
        {
            base.OnMove(e);

            KT.SetWindowPosition((Point)Location);
        }

        protected override void OnUnload()
        {
            KT.Exit();

            base.OnUnload();
        }
    }
}
