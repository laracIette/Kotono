using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    public abstract class DrawableManager<T> where T : IDrawable
    {
        protected readonly List<T> _drawables = new();

        public DrawableManager() { }

        public virtual void Create(T drawable)
        {
            _drawables.Add(drawable);
        }

        public virtual void Delete(T drawable)
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
            for (int i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].Update();
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
