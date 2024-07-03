using Kotono.Utils;

namespace Kotono.Graphics.Objects.Texts
{
    /// <summary>
    /// Settings class for creating a <see cref="Texts.Text"/>.
    /// </summary> 
    internal class TextSettings : Object2DSettings
    {
        /// <summary>
        /// The object that the Text should display.
        /// </summary>
        /// <remarks> 
        /// Default value : null 
        /// </remarks>
        public object? Source { get; set; } = null;

        /// <summary>
        /// The anchor from which the Text should be drawn.
        /// </summary>
        /// <remarks> 
        /// Default value : Anchor.Center 
        /// </remarks>
        public Anchor Anchor { get; set; } = Anchor.Center;

        /// <summary>
        /// The spacing multiplier between each letter of the Text.
        /// </summary>
        /// <remarks> 
        /// Default value : 1.0f 
        /// </remarks>
        public float Spacing { get; set; } = 1.0f;
    }
}
