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

        public virtual Vector Location
        {
            get => _transform.Location;
            set => _transform.Location = value;
        }

        public virtual Vector Rotation
        {
            get => _transform.Rotation;
            set => _transform.Rotation = value;
        }

        public virtual Vector Scale
        {
            get => _transform.Scale;
            set => _transform.Scale = value;
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

        public override void Save()
        {
            if (_settings is Object3DSettings settings)
            {
                
            }

            base.Save();
        }
    }
}
