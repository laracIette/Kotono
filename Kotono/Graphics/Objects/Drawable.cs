using Kotono.Graphics.Shaders;
using Kotono.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Kotono.Graphics.Objects
{
    internal abstract class Drawable : Object, IDrawable, ISaveable, ISelectable
    {
        public string FilePath { get; set; } = string.Empty;

        public virtual bool IsDraw { get; set; } = true;

        public virtual Visibility Visibility { get; set; } = Visibility.EditorAndPlaying;

        public virtual Color Color { get; set; } = Color.White;

        public Viewport Viewport { get; set; } = Viewport.Active;

        public abstract bool IsHovered { get; }

        public bool IsSelected { get; protected set; } = false;

        public bool IsActive => ISelectable.Active == this;

        public List<Drawable> Childrens { get; } = [];

        public virtual Shader Shader => NewLightingShader.Instance;

        public virtual void Draw() { }

        public virtual void Save()
        {
            //JsonParser.WriteFile(this, string.Empty);
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
