using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Kotono.Graphics.Objects
{
    internal abstract class Drawable<TSettings> : Object, IDrawable, ISaveable, ISelectable where TSettings : DrawableSettings
    {
        protected readonly TSettings _settings;

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

        public Viewport Viewport { get; set; } = Viewport.Active;

        public abstract bool IsHovered { get; }

        public bool IsSelected { get; protected set; } = false;

        public bool IsActive => ISelectable.Active == this;

        public IDrawable? Parent { get; set; }

        public List<Drawable> Childrens { get; } = [];

        internal Drawable(TSettings settings) : base() => _settings = settings;

        internal Drawable() : this(Activator.CreateInstance<TSettings>()) { }

        public virtual void Draw() { }

        public virtual void Save()
        {
            JsonParser.WriteFile(_settings);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal TChildren[] GetChildrens<TChildren>() where TChildren : Drawable
        {
            return Childrens.OfType<TChildren>().ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal TChildren? GetChildren<TChildren>() where TChildren : Drawable
        {
            return Childrens.First(Extensions.OfType<TChildren>) as TChildren;
        }
    }
}
