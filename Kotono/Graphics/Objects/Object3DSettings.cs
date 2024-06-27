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
        /// The transform of the <see cref="Object3D"/>.
        /// </summary>
        /// <remarks> 
        /// Default value : Transform.Default 
        /// </remarks>
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
    }
}
