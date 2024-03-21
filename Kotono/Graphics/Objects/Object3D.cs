using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object3D<T> : Drawable<T>, IObject3D where T : Object3DSettings
    {
        private Transform _transform = Transform.Default;

        public virtual Transform Transform
        {
            get => _transform;
            set => _transform = value;
        }

        public virtual Vector RelativeLocation
        {
            get => _transform.RelativeLocation;
            set => _transform.RelativeLocation = value;
        }

        public virtual Vector RelativeRotation
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

        public virtual Vector WorldRotation
        {
            get => _transform.WorldRotation;
            set => _transform.WorldRotation = value;
        }

        public virtual Vector WorldScale
        {
            get => _transform.WorldScale;
            set => _transform.WorldScale = value;
        }

        public virtual Vector LocationVelocity
        {
            get => _transform.LocationVelocity;
            set => _transform.LocationVelocity = value;
        }

        public virtual Vector RotationVelocity
        {
            get => _transform.RotationVelocity;
            set => _transform.RotationVelocity = value;
        }

        public virtual Vector ScaleVelocity
        {
            get => _transform.ScaleVelocity;
            set => _transform.ScaleVelocity = value;
        }

        public IObject3D? Parent { get; private set; } = null;

        internal Object3D(T settings)
            : base(settings)
        {
            _transform = settings.Transform;
        }

        internal Object3D() : base() { }

        public void AttachTo(IObject3D parent)
        {
            Parent = parent;
            Transform.Parent = parent.Transform;
        }

        public void Detach()
        {
            Parent = null;
            Transform.Parent = null;
        }
    }
}
