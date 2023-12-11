using Kotono.Graphics.Objects;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Globalization;

namespace Kotono
{
    public class Window : GameWindow
    {
        private double _stalledTime = 0;
        
        private bool ShouldRenderFrame => IsFocused && (KT.PerformanceWindow.FrameRate < KT.MaxFrameRate);

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
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            KT.MaxFrameRate = windowSettings.MaxFrameRate;

            Path.Kotono = windowSettings.KotonoPath;
            Path.Project = windowSettings.ProjectPath;

            Mouse.CursorState = windowSettings.CursorState;

            GL.ClearColor((Color4)windowSettings.ClearColor);
            
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

            // DRender frame if Window is focused and current FrameRate < desired FrameRate
            if (ShouldRenderFrame)
            {
                _stalledTime = 0;

                KT.PerformanceWindow.AddFrameTime(e.Time);

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                KT.RenderFrame();

                base.SwapBuffers();
            }
            else
            {
                _stalledTime += e.Time;
                KT.PerformanceWindow.AddFrameTime(_stalledTime);
            }
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
