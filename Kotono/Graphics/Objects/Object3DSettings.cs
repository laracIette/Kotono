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
        internal virtual Vector RelativeLocation
        {
            get => _transform.RelativeLocation;
            set => _transform.RelativeLocation = value;
        }

        /// <summary>
        /// The rotation of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Zero 
        /// </remarks>
        internal virtual Rotator RelativeRotation
        {
            get => _transform.RelativeRotation;
            set => _transform.RelativeRotation = value;
        }

        /// <summary>
        /// The scale of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Unit 
        /// </remarks>
        internal virtual Vector RelativeScale
        {
            get => _transform.RelativeScale;
            set => _transform.RelativeScale = value;
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
        internal virtual Rotator RotationVelocity
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
