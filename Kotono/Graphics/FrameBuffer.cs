using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics
{
    internal class FrameBuffer : IDisposable
    {
        private readonly int _frameBuffer;

        private readonly int _colorBufferTexture;

        private readonly int _depthStencilBufferTexture;

        private Point _size = Point.Zero;

        internal Point Size
        {
            get => _size;
            set
            {
                _size = value;
                ResizeFrameBuffer();
            }
        }

        internal FrameBuffer(Point size)
        {
            // Create the color texture
            _colorBufferTexture = GL.GenTexture();

            // Create the depth and stencil texture
            _depthStencilBufferTexture = GL.GenTexture();

            // Create the framebuffer
            _frameBuffer = GL.GenFramebuffer();

            Size = size;
        }

        private void ResizeFrameBuffer()
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

        internal void PreDraw()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBuffer);
            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
        }

        internal void DrawBufferTextures()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            ShaderManager.Color.Draw(_colorBufferTexture);
            ShaderManager.Outline.Draw(_depthStencilBufferTexture);
        }

        public void Dispose()
        {
            GL.DeleteTexture(_depthStencilBufferTexture);
            GL.DeleteTexture(_colorBufferTexture);
            GL.DeleteFramebuffer(_frameBuffer);

            GC.SuppressFinalize(this);
        }
    }
}
