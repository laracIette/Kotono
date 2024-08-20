using Kotono.Graphics.Shaders;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics
{
    internal sealed class Framebuffer : IFramebuffer, IDisposable
    {
        private readonly int _handle;

        private readonly int _colorBufferTextureHandle;

        private readonly int _depthStencilBufferTextureHandle;

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
            _colorBufferTextureHandle = Texture.Gen();

            // Create the depth and stencil texture
            _depthStencilBufferTextureHandle = Texture.Gen();

            // Create the framebuffer
            _handle = GL.GenFramebuffer();

            Size = Window.Size;

            // Attach textures to framebuffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _handle);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _colorBufferTextureHandle, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, TextureTarget.Texture2D, _depthStencilBufferTextureHandle, 0);

            // Check frame buffer completion
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception($"error: Framebuffer isn't complete.");
            }

            // Unbind framebuffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private void ResizeFramebuffer()
        {
            // Update the color texture
            Texture.Bind(_colorBufferTextureHandle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)Size.X, (int)Size.Y, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

            // Update the depth and stencil texture
            Texture.Bind(_depthStencilBufferTextureHandle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Depth24Stencil8, (int)Size.X, (int)Size.Y, 0, PixelFormat.DepthStencil, PixelType.UnsignedInt248, IntPtr.Zero);

            // Unbind texture
            Texture.Bind(0);
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

        private void DrawColor() => ((TextureBufferShader)ShaderManager.Shaders["color"]).Draw(_colorBufferTextureHandle);

        private void DrawOutline() => ((TextureBufferShader)ShaderManager.Shaders["outline"]).Draw(_depthStencilBufferTextureHandle);

        #endregion Helpers

        public void Dispose()
        {
            GL.DeleteTexture(_depthStencilBufferTextureHandle);
            GL.DeleteTexture(_colorBufferTextureHandle);
            GL.DeleteFramebuffer(_handle);

            GC.SuppressFinalize(this);
        }
    }
}
