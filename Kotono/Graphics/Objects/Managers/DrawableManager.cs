using Kotono.Physics;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects.Managers
{
    internal abstract class DrawableManager<T>() where T : Drawable
    {
        internal List<T> Drawables { get; } = [];

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
            foreach (var drawable in Drawables.Where(d => d.IsDraw))
            {
                drawable.Draw();
            }
        }

        internal virtual void Save()
        {
            foreach (var saveable in Drawables.OfType<ISaveable>())
            {
                saveable.Save();
            }
        }

        internal virtual void Dispose()
        {
            foreach (var drawable in Drawables)
            {
                drawable.Dispose();
            }

            Drawables.Clear();
        }
    }
}
