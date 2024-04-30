
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object2D<T> : Drawable<T>, IObject2D where T : Object2DSettings
    {
        private Rect _dest;

        public virtual Rect Dest
        {
            get => _dest;
            set => _dest = value;
        }

        public virtual Point Position
        {
            get => _dest.Position;
            set => _dest.Position = value;
        }

        public virtual Point Size
        {
            get => _dest.Size;
            set => _dest.Size = value;
        }

        public virtual float X
        {
            get => _dest.X;
            set => _dest.X = value;
        }

        public virtual float Y
        {
            get => _dest.Y;
            set => _dest.Y = value;
        }

        public virtual float W
        {
            get => _dest.W;
            set => _dest.W = value;
        }

        public virtual float H
        {
            get => _dest.H;
            set => _dest.H = value;
        }

        public virtual int Layer
        {
            get => _settings.Layer;
            set => _settings.Layer = value;
        }

        internal Object2D(T settings)
            : base(settings)
        {
            Dest = settings.Dest;
        }

        internal Object2D() : base() { }

        public override void Save()
        {
            _settings.Dest = Dest;

            base.Save();
        }
    }
}
