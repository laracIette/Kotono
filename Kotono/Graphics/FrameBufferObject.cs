using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics
{
    internal class FramebufferObject
    {
        private readonly int _handle = GL.GenFramebuffer();

        internal void Bind() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, _handle);

        internal void Delete() => GL.DeleteFramebuffer(_handle);

        internal static void Unbind() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }
}