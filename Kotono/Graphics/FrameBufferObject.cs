using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics
{
    internal sealed class FramebufferObject : IDisposable
    {
        private readonly int _handle = GL.GenFramebuffer();

        internal void Bind() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, _handle);

        internal static void Unbind() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        public void Dispose()
        {
            Unbind();
            GL.DeleteFramebuffer(_handle);
        }
    }
}