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

            GL.Enable(EnableCap.DepthTest);

            new Camera();

            KT.SetWindowPosition((Point)Location);
            KT.SetWindowSize((Point)Size);

            CreateFrameBuffer();

            KT.Init(MouseState, KeyboardState);
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

            // Creating the renderbuffer
            int renderBuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffer);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, (int)KT.Dest.W, (int)KT.Dest.H);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            
            // Attach renderbuffer to framebuffer
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, renderBuffer);

            /* // Depth and stencil buffer
            GL.TexImage2D(
                TextureTarget.Texture2D, 0, PixelInternalFormat.Depth24Stencil8, (int)KT.Dest.W, (int)KT.Dest.H, 0,
                PixelFormat.DepthStencil, PixelType.UnsignedInt248, IntPtr.Zero
            );

            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, TextureTarget.Texture2D, texture, 0);
            */

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
                /* old

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                KT.Draw();
                KT.UpdateShaders();

                base.SwapBuffers();
                */

                // first pass
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBuffer);
                GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // we're not using the stencil buffer now
                GL.Enable(EnableCap.DepthTest);
                KT.Draw();
                KT.UpdateShaders();

                // second pass
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0); // back to default
                GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit);

                GL.BindVertexArray(SquareVertices.VertexArrayObject);
                GL.Disable(EnableCap.DepthTest);
                GL.BindTexture(TextureTarget.Texture2D, _textureColorBuffer);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
                
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

            GL.DeleteFramebuffer(_frameBuffer);

            base.OnUnload();
        }
    }
}
