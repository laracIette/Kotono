using System;
using Kotono.Graphics.Objects;

namespace Kotono
{
    internal abstract class Object : IObject
    {
        internal Object()
        {
            ObjectManager.Create(this);
        }

        public virtual void Update() { }

        public virtual void Delete()
        {
            Dispose();

            ObjectManager.Delete(this);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
