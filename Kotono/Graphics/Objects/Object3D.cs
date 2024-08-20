using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object3D : Drawable, IObject3D
    {
        public virtual Transform Transform { get; } = Transform.Default;

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

        private IObject3D? _parent = null;

        public virtual IObject3D? Parent
        {
            get => _parent;
            set
            {
                using var clone = Transform.Clone();

                _parent = value;
                Transform.Parent = value?.Transform;

                // Remove relative offset, not sure if it'll stay
                Transform.WorldLocation = clone.WorldLocation;
                Transform.WorldRotation = clone.WorldRotation;
                Transform.WorldScale = clone.WorldScale;
            }
        }

        public override bool IsHovered => false;

        public override void Dispose()
        {
            Transform.Dispose();
            
            base.Dispose();
        }
    }
}
