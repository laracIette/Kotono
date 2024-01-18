using Kotono.File;
using Kotono.Graphics.Objects.Managers;
using System;

namespace Kotono.Graphics.Objects
{
    internal abstract class Drawable : IDrawable, IDisposable
    {
        protected DrawableSettings _settings;

        public virtual bool IsDraw { get; set; }

        internal Drawable(DrawableSettings settings)
        {
            _settings = settings;

            IsDraw = _settings.IsDraw;

            ObjectManager.Create(this);
        }

        public virtual void Update() { }

        public virtual void Draw() { }

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
