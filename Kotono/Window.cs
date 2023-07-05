using Kotono.Graphics;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

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

            KT.SetWindowPosition(Location.X, Location.Y);
            KT.SetWindowSize(Size.X, Size.Y);

            KT.Init();

            Input.Update(KeyboardState, MouseState);
        }

        protected new void Load()
        {
            CursorState = CursorState.Normal;
            Input.CursorState = CursorState;
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

            Input.Update(KeyboardState, MouseState);

            Mouse.Update();

            KT.Update();

            if (KeyboardState.IsKeyDown(Input.Escape))
            {
                base.Close();
            }

            if (KeyboardState.IsKeyPressed(Input.Fullscreen))
            {
                WindowState = (WindowState == WindowState.Normal) ?
                    WindowState.Fullscreen :
                    WindowState.Normal;
            }

            if (KeyboardState.IsKeyPressed(Input.GrabMouse))
            {
                //CursorState = (CursorState == CursorState.Normal) ?
                    //CursorState.Grabbed :
                    //CursorState.Normal;

                Input.CursorState = CursorState;
            }

            if (KeyboardState.IsKeyPressed(Keys.S) && KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                KT.Save();
                KT.Print("saved", Color.FromHex("#88FF10"));
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            KT.SetWindowPosition(Location.X, Location.Y);
            KT.SetWindowSize(Size.X, Size.Y);
        }
        bool first = true;
        protected override void OnMove(WindowPositionEventArgs e)
        {
            base.OnMove(e);

            KT.SetWindowPosition(Location.X, Location.Y);
            if (first)
            {
                first = false;
            }
            else
            {
                KT.Print(Location);
            }
        }

        protected override void OnUnload()
        {
            KT.Exit();

            base.OnUnload();
        }
    }
}
