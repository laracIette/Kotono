using Kotono.Graphics.Objects;

namespace Kotono.Settings
{
    /// <summary>
    /// Settings class for creating a <see cref="RoundedBorder"/>.
    /// </summary>
    internal class RoundedBorderSettings : RoundedBoxSettings
    {
        /// <summary>
        /// The thickness of the RoundedBorder.
        /// </summary>
        /// <remarks> 
        /// Default value : 1.0f 
        /// </remarks>
        public float Thickness { get; set; } = 1.0f;
    }
}
