using Kotono.Graphics.Objects.Managers;
using System;

namespace Kotono.Graphics.Objects
{
    public abstract class Drawable : IDrawable, IDisposable
    {
        public virtual bool IsDraw { get; set; } = true;

        public Drawable()
        {
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
