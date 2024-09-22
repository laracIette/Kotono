using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System.Collections.Generic;
using System.Linq;

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

        public Rect Rect { get; } = Rect.Default;

        public virtual Anchor Anchor
        {
            get => Rect.Anchor;
            set => Rect.Anchor = value;
        }

        public virtual Point BaseSize
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

        public virtual int Layer { get; set; } = 0;

        public override bool IsHovered => Rect.Overlaps(Mouse.Position);

        public override bool IsActive => ISelectable2D.Active == this;

        private IObject2D? _parent = null;

        /// <inheritdoc cref="IObject2D.Parent"/>
        /// <remarks>
        /// Changing this will affect the relative rect of the <see cref="Object2D"/> so that it's world rect stays the same.
        /// </remarks>
        public IObject2D? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                {
                    return;
                }

                _parent = value;

                var worldPosition = WorldPosition;
                var worldRotation = WorldRotation;
                var worldSize = WorldSize;

                Rect.Parent = value?.Rect;

                WorldPosition = worldPosition;
                WorldRotation = worldRotation;
                WorldSize = worldSize;
            }
        }

        public IEnumerable<IObject2D> Children => ObjectManager.GetObjectsOfType<IObject2D>(o => o.Parent == this);

        public IEnumerable<TChildren> GetChildren<TChildren>() where TChildren : IObject2D
        {
            return Children.OfType<TChildren>();
        }

        public TChild? GetChild<TChild>() where TChild : IObject2D
        {
            return GetChildren<TChild>().First();
        }

        public override void Dispose()
        {
            foreach (var child in Children)
            {
                child.Parent = Parent;
            }

            Rect.Dispose();

            base.Dispose();
        }
    }
}
