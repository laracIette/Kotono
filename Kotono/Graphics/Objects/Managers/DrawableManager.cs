using Kotono.Physics;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    internal abstract class DrawableManager<T> where T : Drawable
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
            if (!Drawables.Remove(drawable))
            {
                KT.Log($"error: couldn't remove {drawable.GetType().Name} \"{drawable}\" from Drawables.");
            }
        }

        internal virtual void Update()
        {
            // List can change during IDrawable.Update() calls
            for (int i = 0; i < Drawables.Count; i++)
            {
                Drawables[i].Update();

                if (Drawables[i] is IFizixObject { IsFizix: true } obj)
                {
                    obj.UpdateFizix();
                }
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
                (drawable as ISaveable)?.Save();
            }
        }

        public virtual void Dispose()
        {
            // List changes as drawables get deleted
            for (int i = Drawables.Count - 1; i >= 0; i--)
            {
                Drawables[i].Dispose();

                Delete(Drawables[i]);
            }
        }
    }
}
