using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics
{
    internal sealed class VertexBufferObject : IBufferObject
    {
        private readonly int _handle = GL.GenBuffer();

        public void Bind() => GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);

        public void SetData<T>(T[] data, int size) where T : struct
        {
            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * size, data, BufferUsageHint.StaticDraw);
        }

        internal static void Unbind() => GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        public void Dispose()
        {
            Unbind();
            GL.DeleteBuffer(_handle);
        }
    }
}
