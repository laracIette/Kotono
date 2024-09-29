using Kotono.Utils.Coordinates;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object3D : Drawable, IObject3D, ISelectable3D
    {
        private bool _isDraw = true;

        public override bool IsDraw
        {
            get => _isDraw && (Parent?.IsDraw ?? true);
            set => _isDraw = value;
        }

        public Transform Transform { get; } = new();

        public virtual Vector RelativeLocation
        {
            get => Transform.RelativeLocation;
            set => Transform.RelativeLocation = value;
        }

        public virtual Rotator RelativeRotation
        {
            get => Transform.RelativeRotation;
            set => Transform.RelativeRotation = value;
        }

        public virtual Vector RelativeScale
        {
            get => Transform.RelativeScale;
            set => Transform.RelativeScale = value;
        }

        public virtual Vector WorldLocation
        {
            get => Transform.WorldLocation;
            set => Transform.WorldLocation = value;
        }

        public virtual Rotator WorldRotation
        {
            get => Transform.WorldRotation;
            set => Transform.WorldRotation = value;
        }

        public virtual Vector WorldScale
        {
            get => Transform.WorldScale;
            set => Transform.WorldScale = value;
        }

        public virtual Vector RelativeLocationVelocity
        {
            get => Transform.RelativeLocationVelocity;
            set => Transform.RelativeLocationVelocity = value;
        }

        public virtual Rotator RelativeRotationVelocity
        {
            get => Transform.RelativeRotationVelocity;
            set => Transform.RelativeRotationVelocity = value;
        }

        public virtual Vector RelativeScaleVelocity
        {
            get => Transform.RelativeScaleVelocity;
            set => Transform.RelativeScaleVelocity = value;
        }

        public virtual Vector WorldLocationVelocity
        {
            get => Transform.WorldLocationVelocity;
            set => Transform.WorldLocationVelocity = value;
        }

        public virtual Rotator WorldRotationVelocity
        {
            get => Transform.WorldRotationVelocity;
            set => Transform.WorldRotationVelocity = value;
        }

        public virtual Vector WorldScaleVelocity
        {
            get => Transform.WorldScaleVelocity;
            set => Transform.WorldScaleVelocity = value;
        }

        public override bool IsHovered => false;

        public override bool IsActive => ISelectable3D.Active == this;

        private IObject3D? _parent = null;

        /// <inheritdoc cref="IObject3D.Parent"/>
        /// <remarks>
        /// Changing this will affect the relative transform of the <see cref="Object3D"/> so that it's world transform stays the same.
        /// </remarks>
        public virtual IObject3D? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                {
                    return;
                }

                _parent = value;

                var worldLocation = WorldLocation;
                var worldRotation = WorldRotation;
                var worldScale = WorldScale;

                Transform.Parent = value?.Transform;

                WorldLocation = worldLocation;
                WorldRotation = worldRotation;
                WorldScale = worldScale;
            }
        }

        public IEnumerable<IObject3D> Children => ObjectManager.GetObjectsOfType<IObject3D>(o => o.Parent == this);

        public IEnumerable<TChildren> GetChildren<TChildren>() where TChildren : IObject3D
            => Children.OfType<TChildren>();

        public TChild? GetChild<TChild>() where TChild : IObject3D 
            => GetChildren<TChild>().First();

        public override string ToString()
            => $"{base.ToString()}, Transform: {{{Transform}}}";

        public override void Dispose()
        {
            foreach (var child in Children)
            {
                child.Parent = Parent;
            }

            base.Dispose();
        }
    }
}
