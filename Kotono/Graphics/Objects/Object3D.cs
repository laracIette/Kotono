using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object3D : Drawable, IObject3D
    {
        private Transform _transform;

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

        internal Object3D(Object3DSettings settings)
            : base(settings)
        {
            _transform = settings.Transform; 
        }

        internal Object3D() : this(new Object3DSettings()) { }

        public override void Save()
        {
            if (_settings is Object3DSettings settings)
            {

            }

            base.Save();
        }
    }
}
