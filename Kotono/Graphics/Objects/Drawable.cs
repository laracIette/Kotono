using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Objects.Settings;
using System;

namespace Kotono.Graphics.Objects
{
    internal abstract class Drawable : IDrawable, IDisposable
    {
        public virtual bool IsDraw { get; set; }

        internal Drawable(DrawableSettings settings)
        {
            IsDraw = settings.IsDraw;

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
