using Kotono.Utils;

namespace Kotono.File
{
    internal class Object3DSettings : DrawableSettings
    {
        /// <summary>
        /// The location of the Object3D.
        /// <para> Default value : Vector.Zero </para>
        /// </summary>
        [Parsable]
        public Vector Location { get; set; } = Vector.Zero;

        /// <summary>
        /// The rotation of the Object3D.
        /// <para> Default value : Vector.Zero </para>
        /// </summary>
        [Parsable]
        public Vector Rotation { get; set; } = Vector.Zero;

        /// <summary>
        /// The scale of the Object3D.
        /// <para> Default value : Vector.Unit </para>
        /// </summary>
        [Parsable]
        public Vector Scale { get; set; } = Vector.Unit;

        /// <summary>
        /// The velocity of the Object3D.
        /// <para> Default value : Vector.Zero </para>
        /// </summary>
        [Parsable]
        public Vector Velocity { get; set; } = Vector.Zero;
    }
}
