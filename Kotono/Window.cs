using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Statistics;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Globalization;

namespace Kotono
{
    internal abstract class Window : GameWindow
    {
        private float _stalledTime = 0;

        private bool ShouldRenderFrame => IsFocused && (PerformanceWindow.FrameRate < PerformanceWindow.MaxFrameRate);

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

            Mouse.CursorState = windowSettings.CursorState;
            Mouse.MouseState = MouseState;

            Mouse.HideCursor();

            Keyboard.KeyboardState = KeyboardState;

            new Camera();

            KT.Position = (Point)Location;
            KT.Size = (Point)ClientSize;
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

            PerformanceWindow.AddUpdateTime((float)e.Time);

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

            if (Keyboard.IsKeyPressed(Keys.S) && Keyboard.IsKeyDown(Keys.LeftControl))
            {
                KT.Save();
                KT.Print("saved", Color.FromHex("#88FF10"));
            }

            Update();
        }

        protected abstract void Update();

        protected sealed override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            KT.Position = (Point)Location;
            KT.Size = (Point)ClientSize;
        }

        protected sealed override void OnMove(WindowPositionEventArgs e)
        {
            base.OnMove(e);

            KT.Position = (Point)Location;
        }

        protected sealed override void OnUnload()
        {
            KT.Exit();

            base.OnUnload();
        }
    }
}
