using System;

namespace Kotono
{
    internal interface IObject : IDisposable
    {
        public void Update();

        public void Delete();
    }
}
