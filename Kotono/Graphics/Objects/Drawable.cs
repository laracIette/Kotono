using Kotono.Graphics.Objects.Managers;
using System;

namespace Kotono.Graphics.Objects
{
    public abstract class Drawable : IDrawable
    {
        public virtual bool IsDraw { get; set; } = true;

        public Drawable()
        {
            ObjectManager.Create(this);
        }

        public virtual void Update() { }

        public virtual void Draw() { }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

            ObjectManager.Delete(this);
        }
    }
}
