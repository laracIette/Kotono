using Kotono.Settings;
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
