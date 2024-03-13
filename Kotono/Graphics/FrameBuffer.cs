using Kotono.Graphics.Shaders;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics
{
    internal class Framebuffer : IFramebuffer, IDisposable
    {
        private readonly int _framebuffer;

        private readonly int _colorBufferTexture;

        private readonly int _depthStencilBufferTexture;

        private Point _size = Point.Zero;

        internal Point Size
        {
            get => _size;
            set
            {
                if (_size != value)
                {
                    _size = value;
                    ResizeFramebuffer();
                }
            }
        }

        internal Framebuffer()
        {
            // Create the color texture
            _colorBufferTexture = GL.GenTexture();

            // Create the depth and stencil texture
            _depthStencilBufferTexture = GL.GenTexture();

            // Create the framebuffer
            _framebuffer = GL.GenFramebuffer();

            Size = Window.Size;
        }

        private void ResizeFramebuffer()
        {
            // Update the color texture
            GL.BindTexture(TextureTarget.Texture2D, _colorBufferTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)Size.X, (int)Size.Y, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
            SetTextureParameters();

            // Update the depth and stencil texture
            GL.BindTexture(TextureTarget.Texture2D, _depthStencilBufferTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Depth24Stencil8, (int)Size.X, (int)Size.Y, 0, PixelFormat.DepthStencil, PixelType.UnsignedInt248, IntPtr.Zero);
            SetTextureParameters();

            // Unbind texture
            Texture.Unbind();

            // Attach textures to framebuffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _framebuffer);
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

        public void BeginDraw()
        {
            BindFramebuffer();
            SetClearColor(Color._1A1A33FF);
            ClearColorAndDepthBuffers();
        }

        public void DrawBufferTextures()
        {
            UnBindFramebuffer();
            SetClearColor(Color.White);
            ClearColorAndDepthBuffers();

            DrawColor();
            GL.Enable(EnableCap.Blend);
            DrawOutline();
            GL.Disable(EnableCap.Blend);
        }

        #region Helpers

        private void BindFramebuffer() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, _framebuffer);

        private static void UnBindFramebuffer() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        private static void SetClearColor(Color color) => GL.ClearColor((Color4)color);

        private static void ClearColorBuffer() => GL.Clear(ClearBufferMask.ColorBufferBit);

        private static void ClearDepthBuffer() => GL.Clear(ClearBufferMask.DepthBufferBit);

        private static void ClearColorAndDepthBuffers() => GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        private void DrawColor() => ShaderManager.Color.Draw(_colorBufferTexture);

        private void DrawOutline() => ShaderManager.Outline.Draw(_depthStencilBufferTexture);

        #endregion Helpers

        public void Dispose()
        {
            GL.DeleteTexture(_depthStencilBufferTexture);
            GL.DeleteTexture(_colorBufferTexture);
            GL.DeleteFramebuffer(_framebuffer);

            GC.SuppressFinalize(this);
        }
    }
}
