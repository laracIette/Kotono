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
            KT.SetWindowSize(Size.X, Size.Y);

            KT.Init();

            Input.Update(KeyboardState, MouseState);
        }

        protected new void Load()
        {
            CursorState = CursorState.Grabbed;
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

            KT.Update();

            if (Input.MouseState!.IsButtonDown(MouseButton.Left))
            {
                if ((MouseState.X < 0) || (MouseState.X > KT.Width) || (MouseState.Y < 0) || (MouseState.Y > KT.Height))
                {
                    //MouseState.Position = MouseState.PreviousPosition;
                }
            }

            if (Input.KeyboardState!.IsKeyDown(Input.Escape))
            {
                base.Close();
            }

            if (Input.KeyboardState.IsKeyPressed(Input.Fullscreen))
            {
                WindowState = (WindowState == WindowState.Normal) ?
                    WindowState.Fullscreen :
                    WindowState.Normal;
            }

            if (Input.KeyboardState.IsKeyPressed(Input.GrabMouse))
            {
                CursorState = (CursorState == CursorState.Normal) ?
                    CursorState.Grabbed :
                    CursorState.Normal;
                
                Input.CursorState = CursorState;
            }

            if (Input.KeyboardState.IsKeyPressed(Keys.S) && Input.KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                KT.Save();
                KT.Print("saved", Color.FromHex("#88FF10"));
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            KT.SetWindowSize(Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            KT.Exit();

            base.OnUnload();
        }
    }
}
