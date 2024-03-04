using Kotono.Audio;
using Kotono.Engine;
using Kotono.Engine.UserInterface.AddMenu;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Statistics;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Globalization;

namespace Kotono
{
    internal abstract class Window : GameWindow
    {
        private float _stalledTime = 0;

        private bool ShouldRenderFrame => IsFocused && (PerformanceWindow.FrameRate < PerformanceWindow.MaxFrameRate);

        private static Rect _dest = new(0.0f, 0.0f, 1280.0f, 720.0f);

        internal static Rect Dest
        {
            get => _dest;
            set
            {
                Position = value.Position;
                Size = value.Size;
            }
        }

        internal static Point Position
        {
            get => _dest.Position;
            set => _dest.Position = value;
        }

        internal new static Point Size
        {
            get => _dest.Size;
            set
            {
                _dest.Size = value;

                ObjectManager.ActiveCamera.AspectRatio = Size.Ratio;

                ComponentManager.WindowViewport.Size = Size;

                PerformanceWindow.UpdatePosition();

                if (Size > Point.Zero)
                {
                    ObjectManager.SetSize(Size);
                }
            }
        }

        internal Window(WindowSettings windowSettings)
            : base(
                GameWindowSettings.Default,
                new NativeWindowSettings
                {
                    Size = new Vector2i(windowSettings.Width, windowSettings.Height),
                    Title = windowSettings.WindowTitle,
                    StartVisible = false,
                    Location = new Vector2i((1920 - windowSettings.Width) / 2, (1080 - windowSettings.Height) / 2),
                    NumberOfSamples = 1
                }
            )
        {
            // Needed to parse float correctly
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            PerformanceWindow.MaxFrameRate = windowSettings.MaxFrameRate;
            
            SoundManager.GeneralVolume = 1.0f;

            Mouse.CursorState = windowSettings.CursorState;
            Mouse.MouseState = MouseState;

            Mouse.HideCursor();

            Keyboard.KeyboardState = KeyboardState;

            _ = new Camera();

            _ = new MainMenu();

            Position = (Point)Location;
            Size = (Point)ClientSize;
        }

        protected sealed override void OnLoad()
        {
            base.OnLoad();

            IsVisible = true;
        }

        protected sealed override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // Render frame if Window is focused and current FrameRate < desired FrameRate
            if (ShouldRenderFrame)
            {
                _stalledTime = 0.0f;

                PerformanceWindow.AddFrameTime((float)e.Time);

                ShaderManager.Update();

                ObjectManager.Draw();

                base.SwapBuffers();
            }
            else
            {
                _stalledTime += (float)e.Time;
                PerformanceWindow.AddFrameTime(_stalledTime);
            }
        }

        protected sealed override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                base.Close();
            }

            PerformanceWindow.AddUpdateTime((float)e.Time);

            Time.Update();
            Mouse.Update();
            Gizmo.Update();
            ObjectManager.Update();
            ComponentManager.Update();
            StateManager.Update();

            if (Keyboard.IsKeyPressed(Keys.F11))
            {
                WindowState = (WindowState == WindowState.Normal) ?
                    WindowState.Fullscreen :
                    WindowState.Normal;
            }

            if (Keyboard.IsKeyPressed(Keys.S) && Keyboard.IsKeyDown(Keys.LeftControl))
            {
                Save();
                Printer.Print("saved", Color.FromHex("#88FF10"));
            }

            Update();
        }

        protected abstract void Update();

        private static void Save()
        {
            ObjectManager.Save();
        }

        protected sealed override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            Position = (Point)Location;
            Size = (Point)ClientSize;
        }

        protected sealed override void OnMove(WindowPositionEventArgs e)
        {
            base.OnMove(e);

            Position = (Point)Location;
        }

        protected sealed override void OnUnload()
        {
            SoundManager.Dispose();
            Texture.DisposeAll();
            ObjectManager.Dispose();

            base.OnUnload();
        }
    }
}
