using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics
{
    internal sealed class VertexArrayObject : IDisposable
    {
        private readonly int _handle = GL.GenVertexArray();

        internal void Bind() => GL.BindVertexArray(_handle);

        public void Dispose()
        {
            GL.DeleteVertexArray(_handle);
            GC.SuppressFinalize(this);
        }
    }
}
