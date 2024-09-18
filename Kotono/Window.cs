using Kotono.Audio;
using Kotono.Engine.UserInterface.AddMenu;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Shaders;
using Kotono.Graphics.Statistics;
using Kotono.Graphics.Textures;
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
        private static readonly PerformanceWindow _performanceWindow = new()
        {
            RelativeSize = new Point(400.0f, 120.0f),
            Anchor = Anchor.BottomRight,
        };

        private float _stalledTime = 0.0f;

        private static Point _size = Point.Zero;

        private bool ShouldRenderFrame => IsFocused && (_performanceWindow.FrameRate < _performanceWindow.MaxFrameRate);

        internal static Point Position { get; set; } = Point.Zero;

        internal new static Point Size
        {
            get => _size;
            set
            {
                _size = value;

                _performanceWindow.RelativePosition = value;

                WindowComponentManager.WindowViewport.BaseSize = value;

                if (value > Point.Zero)
                {
                    ObjectManager.SetRendererSize(value);
                }
            }
        }

        internal Window(WindowSettings windowSettings)
            : base(
                GameWindowSettings.Default,
                new NativeWindowSettings
                {
                    Size = (Vector2i)windowSettings.WindowSize,
                    Title = windowSettings.WindowTitle,
                    StartVisible = false,
                    Location = ((Vector2i)(1920, 1080) - (Vector2i)windowSettings.WindowSize) / 2,
                    NumberOfSamples = 1
                }
            )
        {
            // Needed to parse correctly
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            Position = (Point)Location;
            Size = (Point)ClientSize;

            _performanceWindow.MaxFrameRate = windowSettings.MaxFrameRate;

            Mouse.CursorState = windowSettings.CursorState;
            Mouse.MouseState = MouseState;

            Mouse.HidePointer();

            Keyboard.KeyboardState = KeyboardState;

            _ = new MainMenu();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            IsVisible = true;

            Start();
        }

        protected abstract void Start();

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // Render frame if Window is focused and current FrameRate < desired FrameRate
            if (ShouldRenderFrame)
            {
                _stalledTime = 0.0f;

                _performanceWindow.AddFrameTime((float)e.Time);

                ShaderManager.Update();

                ObjectManager.Draw();

                base.SwapBuffers();
            }
            else
            {
                _stalledTime += (float)e.Time;
                _performanceWindow.AddFrameTime(_stalledTime);
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                base.Close();
            }

            _performanceWindow.AddUpdateTime((float)e.Time);

            Time.Update();
            Keyboard.Update();
            Mouse.Update();
            ObjectManager.Update();

            if (Keyboard.IsKeyPressed(Keys.F11))
            {
                WindowState = (WindowState == WindowState.Normal)
                    ? WindowState.Fullscreen
                    : WindowState.Normal;
            }

            if (Keyboard.IsKeyPressed(Keys.S) && Keyboard.IsKeyDown(Keys.LeftControl))
            {
                ObjectManager.Save();
                Printer.Print("saved", Color.FromHex("#88FF10"));
            }

            Update();
        }

        protected abstract void Update();

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            Position = (Point)Location;
            Size = (Point)ClientSize;
        }

        protected override void OnMove(WindowPositionEventArgs e)
        {
            base.OnMove(e);

            Position = (Point)Location;
        }

        protected override void OnUnload()
        {
            Source.DisposeAll();
            AudioManager.Dispose();
            ImageTexture.DisposeAll();
            ObjectManager.Dispose();

            base.OnUnload();
        }
    }
}
