using System;

namespace Kotono.Graphics
{
    internal sealed class VertexArraySetup : IDisposable
    {
        internal VertexArrayObject VertexArrayObject { get; } = new();

        internal VertexBufferObject VertexBufferObject { get; } = new();

        /// <summary>
        /// Binds the <see cref="Graphics.VertexArrayObject"/> and the <see cref="Graphics.VertexBufferObject"/>.
        /// </summary>
        internal void Bind()
        {
            VertexArrayObject.Bind();
            VertexBufferObject.Bind();
        }

        /// <summary>
        /// Binds the <see cref="Graphics.VertexArrayObject"/> and sets the <see cref="Graphics.VertexBufferObject"/>'s data.
        /// </summary>
        internal void SetData<T>(T[] data, int size) where T : struct
        {
            VertexArrayObject.Bind();
            VertexBufferObject.SetData(data, size);
        }

        public void Dispose()
        {
            VertexArrayObject.Dispose();
            VertexBufferObject.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
