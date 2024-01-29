﻿using Kotono.Graphics;
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
        private double _stalledTime = 0;

        private int _frameBuffer;

        private int _colorBufferTexture;

        private int _depthStencilBufferTexture;

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
            KT.Size = (Point)Size;

            CreateFrameBuffer();
        }

        private void CreateFrameBuffer()
        {
            // Create the color texture
            _colorBufferTexture = GL.GenTexture();

            // Create the depth and stencil texture
            _depthStencilBufferTexture = GL.GenTexture();

            // Create the framebuffer
            _frameBuffer = GL.GenFramebuffer();

            ResizeFrameBuffer();
        }

        private void ResizeFrameBuffer()
        {
            // Update the color texture
            GL.BindTexture(TextureTarget.Texture2D, _colorBufferTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)KT.Dest.W, (int)KT.Dest.H, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
            SetTextureParameters();

            // Update the depth and stencil texture
            GL.BindTexture(TextureTarget.Texture2D, _depthStencilBufferTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Depth24Stencil8, (int)KT.Dest.W, (int)KT.Dest.H, 0, PixelFormat.DepthStencil, PixelType.UnsignedInt248, IntPtr.Zero);
            SetTextureParameters();

            // Unbind texture
            GL.BindTexture(TextureTarget.Texture2D, 0);

            // Attach textures to framebuffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBuffer);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _colorBufferTexture, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, TextureTarget.Texture2D, _depthStencilBufferTexture, 0);

            // Check frame buffer completion
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception($"error: Framebuffer isn't complete.");
            }

            // Unbind framebuffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private static void SetTextureParameters()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
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
                _stalledTime = 0;

                PerformanceWindow.AddFrameTime(e.Time);

                ShaderManager.Update();

                ObjectManager.Draw();

                // TODO: separate object 3d and 2d manager ????????
                //       to draw 3d before then depth buffer then draw 2d??????

                base.SwapBuffers();
            }
            else
            {
                _stalledTime += e.Time;
                PerformanceWindow.AddFrameTime(_stalledTime);
            }
        }

        protected sealed override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            PerformanceWindow.AddUpdateTime(e.Time);

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

            if (KT.Size > Point.Zero)
            {
                ResizeFrameBuffer();
            }
        }

        protected sealed override void OnMove(WindowPositionEventArgs e)
        {
            base.OnMove(e);

            KT.Position = (Point)Location;
        }

        protected sealed override void OnUnload()
        {
            KT.Exit();

            GL.DeleteTexture(_depthStencilBufferTexture);
            GL.DeleteTexture(_colorBufferTexture);
            GL.DeleteFramebuffer(_frameBuffer);

            base.OnUnload();
        }
    }
}
