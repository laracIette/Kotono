using Kotono.Graphics.Objects;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono
{
    public class Window : GameWindow
    {
        private double _stalledTime = 0;

        public Window(WindowSettings windowSettings)
            : base(
                  GameWindowSettings.Default, 
                  new NativeWindowSettings()
                  { 
                      Size = new Vector2i(windowSettings.Width, windowSettings.Height),
                      Title = windowSettings.WindowTitle,
                      StartVisible = false,
                      Location = new Vector2i((1920 - windowSettings.Width) / 2, (1080 - windowSettings.Height) / 2),
                      NumberOfSamples = 1
                  }
              )
        {
            KT.MaxFrameRate = windowSettings.MaxFrameRate;

            Path.Kotono = windowSettings.KotonoPath;
            Path.Project = windowSettings.ProjectPath;

            Mouse.CursorState = windowSettings.CursorState;

            GL.ClearColor(windowSettings.ClearColor.R, windowSettings.ClearColor.G, windowSettings.ClearColor.B, windowSettings.ClearColor.A);

            GL.Enable(EnableCap.DepthTest);

            new Camera();

            KT.SetWindowPosition((Point)Location);
            KT.SetWindowSize((Point)Size);

            KT.Init(MouseState, KeyboardState);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            IsVisible = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // don't render frame if current FrameRate > desired FrameRate
            if (!IsFocused || (KT.PerformanceWindow.FrameRate > KT.MaxFrameRate - 1))
            {
                _stalledTime += e.Time;
                KT.PerformanceWindow.AddFrameTime(_stalledTime);
                return;
            }

            _stalledTime = 0;

            KT.PerformanceWindow.AddFrameTime(e.Time);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            KT.RenderFrame();

            base.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KT.PerformanceWindow.AddUpdateTime(e.Time);

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
