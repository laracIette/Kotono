using System;

namespace Kotono.Graphics
{
    internal interface IBufferObject : IDisposable
    {
        public void Bind();

        public void SetData<T>(T[] data, int size) where T : struct;
    }
}
