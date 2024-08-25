using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics
{
    internal sealed class ElementBufferObject
    {
        private readonly int _handle = GL.GenBuffer();

        internal void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);

        internal void SetData<T>(T[] data, int size) where T : struct
        {
            Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * size, data, BufferUsageHint.StaticDraw);
        }
    }
}
