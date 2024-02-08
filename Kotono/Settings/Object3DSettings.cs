using Kotono.Utils;
using Kotono.Graphics.Objects;

namespace Kotono.Settings
{
    /// <summary>
    /// Settings class for creating an <see cref="Object3D"/>.
    /// </summary>
    internal class Object3DSettings : DrawableSettings
    {
        /// <summary>
        /// The location of the Object3D.
        /// <para> Default value : Vector.Zero </para>
        /// </summary>
        public Vector Location { get; set; } = Vector.Zero;

        /// <summary>
        /// The rotation of the Object3D.
        /// <para> Default value : Vector.Zero </para>
        /// </summary>
        public Vector Rotation { get; set; } = Vector.Zero;

        /// <summary>
        /// The scale of the Object3D.
        /// <para> Default value : Vector.Unit </para>
        /// </summary>
        public Vector Scale { get; set; } = Vector.Unit;

        /// <summary>
        /// The velocity of the Object3D.
        /// <para> Default value : Vector.Zero </para>
        /// </summary>
        public Vector Velocity { get; set; } = Vector.Zero;
    }
}
