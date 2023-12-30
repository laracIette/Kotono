using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public abstract class Object2D()
        : Drawable(),
        IObject2D
    {
        private Rect _dest = Rect.Zero;

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

        private int _layer = 0;

        public virtual int Layer 
        {
            get => _layer;
            set
            {
                _layer = value;
                ObjectManager.UpdateObject2DLayer(this);
            }
        }
    }
}
