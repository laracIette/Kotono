using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    /// <summary>
    /// Settings class for creating an <see cref="Object3D"/>.
    /// </summary>
    internal class Object3DSettings : DrawableSettings
    {
        /// <summary>
        /// The location of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Zero 
        /// </remarks>
        public Vector Location { get; set; } = Vector.Zero;

        /// <summary>
        /// The rotation of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Zero 
        /// </remarks>
        public Vector Rotation { get; set; } = Vector.Zero;

        /// <summary>
        /// The scale of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Unit 
        /// </remarks>
        public Vector Scale { get; set; } = Vector.Unit;

        /// <summary>
        /// The velocity of the Object3D.
        /// </summary>
        /// <remarks> 
        /// Default value : Vector.Zero 
        /// </remarks>
        public Vector Velocity { get; set; } = Vector.Zero;
    }
}
