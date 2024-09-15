using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics
{
    internal sealed class ElementBufferObject : IBufferObject
    {
        private readonly int _handle = GL.GenBuffer();

        public void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);

        public void SetData<T>(T[] data, int size) where T : struct
        {
            Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * size, data, BufferUsageHint.StaticDraw);
        }

        internal static void Unbind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

        public void Dispose()
        {
            Unbind();
            GL.DeleteBuffer(_handle);
        }
    }
}
