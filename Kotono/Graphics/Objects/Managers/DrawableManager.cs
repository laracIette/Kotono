﻿using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    internal abstract class DrawableManager<T> where T : IDrawable
    {
        protected readonly List<T> _drawables = new();

        internal DrawableManager() { }

        internal virtual void Create(T drawable)
        {
            if (!_drawables.Contains(drawable))
            {
                _drawables.Add(drawable);
            }
        }

        internal virtual void Delete(T drawable)
        {
            drawable.Dispose();
            _drawables.Remove(drawable);
        }

        internal virtual void Init()
        {
            // List can change during IDrawable.Init() calls
            for (int i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].Init();
            }
        }

        internal virtual void Update()
        {
            // List can change during IDrawable.Update() calls
            for (int i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].Update();
            }
        }

        internal virtual void UpdateShaders()
        {
            foreach (var drawable in _drawables)
            {
                drawable.UpdateShaders();
            }
        }

        internal virtual void Draw()
        {
            // List shouldn't change during IDrawable.Draw() calls, TODO: use foreach when development done
            for (int i = 0; i < _drawables.Count; i++)
            {
                if (_drawables[i].IsDraw)
                {
                    _drawables[i].Draw();
                }
            }
        }

        internal virtual void Save()
        {
            foreach (var drawable in _drawables)
            {
                drawable.Save();
            }
        }
    }
}
