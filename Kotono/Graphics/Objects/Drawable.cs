using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects
{
    internal abstract class Drawable<T> : Object, IDrawable, ISaveable, ISelectable where T : DrawableSettings
    {
        protected readonly T _settings;

        internal string SettingsPath
        {
            get => _settings.Path;
            set => _settings.Path = value;
        }

        public virtual bool IsDraw
        {
            get => _settings.IsDraw;
            set => _settings.IsDraw = value;
        }

        public virtual Color Color
        {
            get => _settings.Color;
            set => _settings.Color = value;
        }

        public virtual bool IsHovered { get; } = false;

        public bool IsSelected { get; protected set; } = false;

        public bool IsActive => ISelectable.Active == this;

        internal Drawable(T settings)
            : base()
        {
            _settings = settings;
        }

        internal Drawable() : this(Activator.CreateInstance<T>()) { }

        public virtual void Draw() { }

        public virtual void Save()
        {
            JsonParser.WriteFile(_settings);
        }
    }
}
