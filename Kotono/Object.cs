using System;
using Kotono.Graphics.Objects;

namespace Kotono
{
    internal abstract class Object : IObject
    {
        protected readonly ObjectSettings _settings;

        internal Object(ObjectSettings settings)
        {
            _settings = settings;

            ObjectManager.Create(this);
        }

        internal Object() : this(new ObjectSettings()) { }

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
