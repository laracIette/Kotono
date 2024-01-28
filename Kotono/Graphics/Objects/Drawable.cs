using Kotono.File;
using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects
{
    internal abstract class Drawable : IDrawable, ISaveable, IDisposable
    {
        protected DrawableSettings _settings;

        public virtual bool IsDraw { get; set; }

        public virtual Color Color { get; set; }

        internal Drawable(DrawableSettings settings)
        {
            _settings = settings;

            IsDraw = _settings.IsDraw;
            Color = _settings.Color;

            ObjectManager.Create(this);
        }

        public virtual void Update() { }

        public virtual void Draw() { }

        public virtual void Save() 
        { 
            _settings.IsDraw = IsDraw;
            _settings.Color = Color;

            Settings.WriteFile(_settings);
        }

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
