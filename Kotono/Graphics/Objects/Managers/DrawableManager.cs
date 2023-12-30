using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    internal abstract class DrawableManager<T> where T : IDrawable
    {
        internal List<T> Drawables { get; } = [];

        internal DrawableManager() { }

        internal virtual void Create(T drawable)
        {
            if (!Drawables.Contains(drawable))
            {
                Drawables.Add(drawable);
            }
        }

        internal virtual void Delete(T drawable)
        {
            drawable.Dispose();
            Drawables.Remove(drawable);
        }

        internal virtual void Update()
        {
            // List can change during IDrawable.Update() calls
            for (int i = 0; i < Drawables.Count; i++)
            {
                Drawables[i].Update();
            }
        }

        internal virtual void Draw()
        {
            // List shouldn't change during IDrawable.Draw() calls, TODO: use foreach when development done
            for (int i = 0; i < Drawables.Count; i++)
            {
                if (Drawables[i].IsDraw)
                {
                    Drawables[i].Draw();
                }
            }
        }

        internal virtual void Save()
        {
            foreach (var drawable in Drawables)
            {
                drawable.Save();
            }
        }
    }
}
