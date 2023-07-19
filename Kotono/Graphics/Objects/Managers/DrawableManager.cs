using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    public abstract class DrawableManager
    {
        protected readonly List<IDrawable> _drawables = new();

        public DrawableManager() { }

        public virtual void Create(IDrawable drawable)
        {
            _drawables.Add(drawable);
        }

        public virtual void Delete(IDrawable drawable)
        {
            drawable.Dispose();
            _drawables.Remove(drawable);
        }

        public virtual void Init()
        {
            foreach (var drawable in _drawables)
            {
                drawable.Init();
            }
        }

        public virtual void Update()
        {
            foreach (var drawable in _drawables)
            {
                drawable.Update();
            }
        }

        public virtual void UpdateShaders()
        {
            foreach (var drawable in _drawables)
            {
                drawable.UpdateShaders();
            }
        }

        public virtual void Draw()
        {
            foreach (var drawable in _drawables)
            {
                if (drawable.IsDraw)
                {
                    drawable.Draw();
                }
            }
        }

        public virtual void Save()
        {
            foreach (var drawable in _drawables)
            {
                drawable.Save();
            }
        }
    }
}
