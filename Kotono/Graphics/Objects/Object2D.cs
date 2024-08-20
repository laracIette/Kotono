
using Kotono.Input;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object2D : Drawable, IObject2D
    {
        public virtual Rect Rect { get; } = Rect.Default;

        public Point BaseSize
        {
            get => Rect.BaseSize;
            set => Rect.BaseSize = value;
        }

        public virtual Point Size
        {
            get => Rect.Size;
            set => Rect.Size = value;
        }

        public virtual Point Position
        {
            get => Rect.Position;
            set => Rect.Position = value;
        }

        public virtual Rotator Rotation
        {
            get => Rect.Rotation;
            set => Rect.Rotation = value;
        }

        public virtual Point Scale
        {
            get => Rect.Scale;
            set => Rect.Scale = value;
        }

        public virtual int Layer { get; set; } = 0;

        public override bool IsHovered => Rect.Overlaps(Rect, Mouse.Position);

        public IObject2D? Parent { get; set; }

        public override void Dispose()
        {
            Rect.Dispose();

            base.Dispose();
        }
    }
}
