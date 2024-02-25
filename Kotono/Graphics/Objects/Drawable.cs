using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    internal abstract class Drawable : Object, IDrawable, ISaveable, ISelectable
    {
        private readonly string _path;

        public virtual bool IsDraw { get; set; }

        public virtual Color Color { get; set; }

        public bool IsSelected { get; protected set; } = false;

        public bool IsActive => ISelectable.Active == this;

        internal Drawable(DrawableSettings settings)
            : base(settings)
        {
            _path = settings.Path;
            IsDraw = settings.IsDraw;
            Color = settings.Color;
        }

        public virtual void Draw() { }

        public virtual void Save() 
        { 
            if (_settings is DrawableSettings settings)
            {
                settings.Path = _path;
                settings.IsDraw = IsDraw;
                settings.Color = Color;

                JsonParser.WriteFile(settings);
            }
        }
    }
}
