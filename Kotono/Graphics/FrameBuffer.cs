using Kotono.Graphics.Shaders;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics
{
    internal sealed class Framebuffer : IFramebuffer, IDisposable
    {
        private readonly int _handle;

        private readonly Texture _colorBufferTexture = new();

        private readonly Texture _depthStencilBufferTexture = new();

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
            // Create the framebuffer
            _handle = GL.GenFramebuffer();

            Size = Window.Size;
            Size = (1800, 900);
            // Attach textures to framebuffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _handle);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _colorBufferTexture.Handle, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, TextureTarget.Texture2D, _depthStencilBufferTexture.Handle, 0);

            // Check frame buffer completion
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                throw new KotonoException($"Framebuffer isn't complete");
            }

            // Unbind framebuffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private void ResizeFramebuffer()
        {
            // Update the color texture
            _colorBufferTexture.Bind();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)Size.X, (int)Size.Y, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

            // Update the depth and stencil texture
            _depthStencilBufferTexture.Bind();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Depth24Stencil8, (int)Size.X, (int)Size.Y, 0, PixelFormat.DepthStencil, PixelType.UnsignedInt248, IntPtr.Zero);

            // Unbind texture
            ITexture.Unbind();
        }

        public void BeginDraw()
        {
            BindFramebuffer();
            SetClearColor(Color._1A1A33FF);
            ClearColorAndDepthBuffers();
        }

        public void DrawBufferTextures()
        {
            UnbindFramebuffer();
            SetClearColor(Color.White);
            ClearColorAndDepthBuffers();

            DrawColor();
            GL.Enable(EnableCap.Blend);
            DrawOutline();
            GL.Disable(EnableCap.Blend);
        }

        #region Helpers

        private void BindFramebuffer() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, _handle);

        private static void UnbindFramebuffer() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        private static void SetClearColor(Color color) => GL.ClearColor((Color4)color);

        private static void ClearColorBuffer() => GL.Clear(ClearBufferMask.ColorBufferBit);

        private static void ClearDepthBuffer() => GL.Clear(ClearBufferMask.DepthBufferBit);

        private static void ClearColorAndDepthBuffers() => GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        private void DrawColor() => ((TextureBufferShader)ShaderManager.Shaders["color"]).Draw(_colorBufferTexture);

        private void DrawOutline() => ((TextureBufferShader)ShaderManager.Shaders["outline"]).Draw(_depthStencilBufferTexture);

        #endregion Helpers

        public void Dispose()
        {
            _depthStencilBufferTexture.Delete();
            _colorBufferTexture.Delete();

            GL.DeleteFramebuffer(_handle);

            GC.SuppressFinalize(this);
        }
    }
}
