using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object3D<T> : Drawable<T>, IObject3D where T : Object3DSettings
    {
        private Transform _transform = Transform.Default;

        public virtual Transform Transform
        {
            get => _transform;
            set
            {
                if (!ReferenceEquals(_transform, value))
                {
                    _transform.Dispose();
                    _transform = value;
                }
            }
        }

        public virtual Vector RelativeLocation
        {
            get => _transform.RelativeLocation;
            set => _transform.RelativeLocation = value;
        }

        public virtual Rotator RelativeRotation
        {
            get => _transform.RelativeRotation;
            set => _transform.RelativeRotation = value;
        }

        public virtual Vector RelativeScale
        {
            get => _transform.RelativeScale;
            set => _transform.RelativeScale = value;
        }

        public virtual Vector WorldLocation
        {
            get => _transform.WorldLocation;
            set => _transform.WorldLocation = value;
        }

        public virtual Rotator WorldRotation
        {
            get => _transform.WorldRotation;
            set => _transform.WorldRotation = value;
        }

        public virtual Vector WorldScale
        {
            get => _transform.WorldScale;
            set => _transform.WorldScale = value;
        }

        public virtual Vector RelativeLocationVelocity
        {
            get => _transform.RelativeLocationVelocity;
            set => _transform.RelativeLocationVelocity = value;
        }

        public virtual Rotator RelativeRotationVelocity
        {
            get => _transform.RelativeRotationVelocity;
            set => _transform.RelativeRotationVelocity = value;
        }

        public virtual Vector RelativeScaleVelocity
        {
            get => _transform.RelativeScaleVelocity;
            set => _transform.RelativeScaleVelocity = value;
        }

        public virtual Vector WorldLocationVelocity
        {
            get => _transform.WorldLocationVelocity;
            set => _transform.WorldLocationVelocity = value;
        }

        public virtual Rotator WorldRotationVelocity
        {
            get => _transform.WorldRotationVelocity;
            set => _transform.WorldRotationVelocity = value;
        }

        public virtual Vector WorldScaleVelocity
        {
            get => _transform.WorldScaleVelocity;
            set => _transform.WorldScaleVelocity = value;
        }

        private IObject3D? _parent = null;

        public new virtual IObject3D? Parent
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

        internal Object3D(T settings)
            : base(settings)
        {
            Transform = settings.Transform;
        }

        internal Object3D() : base() { }

        public override void Save()
        {
            _settings.Transform = Transform;

            base.Save();
        }

        public override void Dispose()
        {
            Transform.Dispose();

            base.Dispose();
        }
    }
}
