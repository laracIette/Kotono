
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object2D<T> : Drawable<T>, IObject2D where T : Object2DSettings
    {
        private Rect _Rect;

        public virtual Rect Rect
        {
            get => _Rect;
            set => _Rect = value;
        }

        public virtual Point Position
        {
            get => _Rect.Position;
            set => _Rect.Position = value;
        }

        public virtual Point Size
        {
            get => _Rect.Size;
            set => _Rect.Size = value;
        }

        public virtual int Layer
        {
            get => _settings.Layer;
            set => _settings.Layer = value;
        }

        internal Object2D(T settings)
            : base(settings)
        {
            Rect = settings.Rect;
        }

        internal Object2D() : base() { }

        public override void Save()
        {
            _settings.Rect = Rect;

            base.Save();
        }
    }
}
