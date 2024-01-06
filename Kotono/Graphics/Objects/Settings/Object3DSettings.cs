using Kotono.Utils;

namespace Kotono.Graphics.Objects.Settings
{
    internal class Object3DSettings : DrawableSettings
    {
        /// <summary>
        /// The transform of the Object3D.
        /// <para> Default value : Transform.Default </para>
        /// </summary>
        internal Transform Transform { get; set; } = Transform.Default;

        /// <summary>
        /// The velocity of the Object3D.
        /// <para> Default value : Vector.Zero </para>
        /// </summary>
        internal Vector Velocity { get; set; } = Vector.Zero;
    }
}
