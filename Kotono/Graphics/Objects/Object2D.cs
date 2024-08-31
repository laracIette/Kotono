
using Kotono.Input;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object2D : Drawable, IObject2D, ISelectable2D
    {
        private bool _isDraw = true;

        public override bool IsDraw
        {
            get => _isDraw && (Parent?.IsDraw ?? true);
            set => _isDraw = value;
        }

        public virtual Rect Rect { get; } = Rect.Default;

        public Point BaseSize
        {
            get => Rect.BaseSize;
            set => Rect.BaseSize = value;
        }

        public virtual Point RelativeSize
        {
            get => Rect.RelativeSize;
            set => Rect.RelativeSize = value;
        }

        public virtual Point RelativePosition
        {
            get => Rect.RelativePosition;
            set => Rect.RelativePosition = value;
        }

        public virtual Rotator RelativeRotation
        {
            get => Rect.RelativeRotation;
            set => Rect.RelativeRotation = value;
        }

        public virtual Point RelativeScale
        {
            get => Rect.RelativeScale;
            set => Rect.RelativeScale = value;
        }

        public virtual Point WorldSize
        {
            get => Rect.WorldSize;
            set => Rect.WorldSize = value;
        }

        public virtual Point WorldPosition
        {
            get => Rect.WorldPosition;
            set => Rect.WorldPosition = value;
        }

        public virtual Rotator WorldRotation
        {
            get => Rect.WorldRotation;
            set => Rect.WorldRotation = value;
        }

        public virtual Point WorldScale
        {
            get => Rect.WorldScale;
            set => Rect.WorldScale = value;
        }

        private IObject2D? _parent = null;

        public IObject2D? Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                Rect.Parent = value?.Rect;
            }
        }

        public virtual int Layer { get; set; } = 0;

        public override bool IsHovered => Rect.Overlaps(Rect, Mouse.Position);

        public override bool IsActive => ISelectable2D.Active == this;

        public override void Dispose()
        {
            Rect.Dispose();

            base.Dispose();
        }
    }
}
