using Kotono.Graphics.Objects;

namespace Kotono.File
{
    /// <summary>
    /// Settings class for creating a <see cref="RoundedBox"/>.
    /// </summary>
    internal class RoundedBoxSettings : Object2DSettings
    {
        /// <summary>
        /// The fall off of the RoundedBox.
        /// <para> Default value : 0.0f </para>
        /// </summary>
        public float FallOff { get; set; } = 0.0f;

        /// <summary>
        /// The corner size of the RoundedBox.
        /// <para> Default value : 0.0f </para>
        /// </summary>
        public float CornerSize { get; set; } = 0.0f;
    }
}
