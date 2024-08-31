using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics
{
    internal sealed class VertexBufferObject : IDisposable
    {
        private readonly int _handle = GL.GenBuffer();

        internal void Bind() => GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);

        internal void SetData<T>(T[] data, int size) where T : struct
        {
            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * size, data, BufferUsageHint.StaticDraw);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
            GC.SuppressFinalize(this);
        }
    }
}
