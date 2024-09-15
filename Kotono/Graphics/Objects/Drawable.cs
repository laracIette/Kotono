using Kotono.Graphics.Shaders;
using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    internal abstract class Drawable : Object, IDrawable, ISaveable, ISelectable
    {
        public string Name { get; set; } = string.Empty;

        public virtual bool IsDraw { get; set; } = true;

        public virtual Visibility Visibility { get; set; } = Visibility.EditorAndPlaying;

        public virtual Color Color { get; set; } = Color.White;

        public virtual Shader Shader { get; set; } = LightingShader.Instance;

        public Viewport Viewport { get; set; } = Viewport.Active;

        public abstract bool IsHovered { get; }

        public bool IsSelected { get; set; } = false;

        public abstract bool IsActive { get; }

        public virtual void UpdateShader() => Shader.Use();

        public virtual void Draw() { }

        public virtual void Save() => JsonParser.WriteFile(this, Path.FromData($@"{Guid}.json"));

        public override string ToString() => $"{Name}: {GetType().Name}";
    }
}
