using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object3D : Drawable, IObject3D
    {
        private Transform _transform = Transform.Default;

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

        public virtual Vector Velocity { get; set; }

        internal Object3D(Object3DSettings settings)
            : base(settings)
        {
            Location = settings.Location;
            Rotation = settings.Rotation;
            Scale = settings.Scale;
            Velocity = settings.Velocity;
        }

        public override void Save()
        {
            if (_settings is Object3DSettings settings)
            {
                settings.Location = Location;
                settings.Rotation = Rotation;
                settings.Scale = Scale;
                settings.Velocity = Velocity;
            }

            base.Save();
        }
    }
}
