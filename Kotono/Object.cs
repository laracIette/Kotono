using Kotono.File;
using Kotono.Graphics.Objects.Managers;
using System;

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
