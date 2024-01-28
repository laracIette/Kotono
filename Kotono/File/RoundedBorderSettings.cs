using Kotono.Graphics.Objects;

namespace Kotono.File
{
    /// <summary>
    /// Settings class for creating a <see cref="RoundedBorder"/>.
    /// </summary>
    internal class RoundedBorderSettings : RoundedBoxSettings
    {
        /// <summary>
        /// The thickness of the RoundedBorder.
        /// <para> Default value : 1.0f </para>
        /// </summary>
        public float Thickness { get; set; } = 1.0f;
    }
}
