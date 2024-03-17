using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects
{
    internal abstract class Drawable<T> : Object, IDrawable, ISaveable, ISelectable where T : DrawableSettings
    {
        protected readonly T _settings;

        internal string SettingsPath { get; set; }

        public virtual bool IsDraw { get; set; }

        public virtual Color Color { get; set; }

        public virtual bool IsHovered { get; } = false;

        public bool IsSelected { get; protected set; } = false;

        public bool IsActive => ISelectable.Active == this;

        internal Drawable(T settings)
            : base()
        {
            _settings = settings;

            SettingsPath = settings.Path;
            IsDraw = settings.IsDraw;
            Color = settings.Color;
        }

        internal Drawable() : this(Activator.CreateInstance<T>()) { }

        public virtual void Draw() { }

        public virtual void Save()
        {
            _settings.Path = SettingsPath;
            _settings.IsDraw = IsDraw;
            _settings.Color = Color;

            JsonParser.WriteFile(_settings);
        }
    }
}
