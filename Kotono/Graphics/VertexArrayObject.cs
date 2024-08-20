using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics
{
    internal sealed class VertexArrayObject
    {
        private readonly int _handle = GL.GenVertexArray();

        internal void Bind() => GL.BindVertexArray(_handle);
    }
}
