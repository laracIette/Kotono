using Kotono.Graphics;
using Kotono.Graphics.Objects;
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
    public class Window : GameWindow
    {
        private double _stalledTime = 0;

        private int _frameBuffer;

        private int _textureColorBuffer;

        private int _textureDepthStencilBuffer;

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
            // For Kotono.Utils.Properties, needed to parse float correctly
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            KT.MaxFrameRate = windowSettings.MaxFrameRate;

            Path.Kotono = windowSettings.KotonoPath;
            Path.Project = windowSettings.ProjectPath;

            Mouse.CursorState = windowSettings.CursorState;
            Mouse.MouseState = MouseState;

            Keyboard.KeyboardState = KeyboardState;

            new Camera();

            KT.Position = (Point)Location;
            KT.Size = (Point)Size;

            CreateFrameBuffer();

            KT.Init();
        }

        private void CreateFrameBuffer()
        {
            // Creating the framebuffer
            _frameBuffer = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBuffer);

            // Creating the color texture
            _textureColorBuffer = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _textureColorBuffer);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)KT.Dest.W, (int)KT.Dest.H, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            // Attach texture to framebuffer
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _textureColorBuffer, 0);

            // Creating the depth and stencil texture
            _textureDepthStencilBuffer = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _textureDepthStencilBuffer);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Depth24Stencil8, (int)KT.Dest.W, (int)KT.Dest.H, 0, PixelFormat.DepthStencil, PixelType.UnsignedInt248, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            // Attach texture to framebuffer
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, TextureTarget.Texture2D, _textureDepthStencilBuffer, 0);

            // Check framebuffer completion
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception($"error: Framebuffer isn't complete.");
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            IsVisible = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // Render frame if Window is focused and current FrameRate < desired FrameRate
            if (ShouldRenderFrame)
            {
                _stalledTime = 0;

                KT.PerformanceWindow.AddFrameTime(e.Time);

                // first pass
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBuffer);
                GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.Enable(EnableCap.DepthTest);

                KT.Draw();

                // second pass
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0); // back to default
                GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                ShaderManager.Color.Draw(_textureColorBuffer);
                ShaderManager.Outline.Draw(_textureDepthStencilBuffer);

                // TODO: separate object 3d and 2d manager ????????
                // TODO: to draw 3d before then depth buffer shit then draw 2d??????

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

            KT.Position = (Point)Location;
            KT.Size = (Point)Size;
        }

        protected override void OnMove(WindowPositionEventArgs e)
        {
            base.OnMove(e);

            KT.Position = (Point)Location;
        }

        protected override void OnUnload()
        {
            KT.Exit();

            GL.DeleteTexture(_textureDepthStencilBuffer);
            GL.DeleteTexture(_textureColorBuffer);
            GL.DeleteFramebuffer(_frameBuffer);

            base.OnUnload();
        }
    }
}
