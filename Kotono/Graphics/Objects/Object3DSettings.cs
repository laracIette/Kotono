using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    /// <summary>
    /// Settings class for creating an <see cref="Object3D"/>.
    /// </summary>
    internal class Object3DSettings : DrawableSettings
    {
        private Transform _transform = Transform.Default;

        /// <summary>
        /// The transform of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Transform.Default 
        /// </remarks>
        public virtual Transform Transform
        {
            get => _transform;
            set => _transform = value;
        }

        /// <summary>
        /// The location of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Zero 
        /// </remarks>
        internal virtual Vector Location
        {
            get => _transform.Location;
            set => _transform.Location = value;
        }

        /// <summary>
        /// The rotation of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Zero 
        /// </remarks>
        internal virtual Vector Rotation
        {
            get => _transform.Rotation;
            set => _transform.Rotation = value;
        }

        /// <summary>
        /// The scale of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Unit 
        /// </remarks>
        internal virtual Vector Scale
        {
            get => _transform.Scale;
            set => _transform.Scale = value;
        }

        /// <summary>
        /// The velocity of the Object3D's location.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Zero 
        /// </remarks>
        internal virtual Vector LocationVelocity
        {
            get => _transform.LocationVelocity;
            set => _transform.LocationVelocity = value;
        }

        /// <summary>
        /// The velocity of the Object3D's rotation.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Zero 
        /// </remarks>
        internal virtual Vector RotationVelocity
        {
            get => _transform.RotationVelocity;
            set => _transform.RotationVelocity = value;
        }

        /// <summary>
        /// The velocity of the Object3D's scale.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Zero 
        /// </remarks>
        internal virtual Vector ScaleVelocity
        {
            get => _transform.ScaleVelocity;
            set => _transform.ScaleVelocity = value;
        }
    }
}
