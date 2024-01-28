using Kotono.Utils;

namespace Kotono.File
{
    /// <summary>
    /// Settings class for creating a <see cref="Graphics.Objects.Texts.Text"/>.
    /// </summary> 
    internal class TextSettings : Object2DSettings
    {
        /// <summary>
        /// The text that the Text should display.
        /// <para> Default value : "" </para>
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// The anchor from which the Text should be drawn.
        /// <para> Default value : Anchor.Center </para>
        /// </summary>
        public Anchor Anchor { get; set; } = Anchor.Center;

        /// <summary>
        /// The spacing multiplier between each letter of the Text.
        /// <para> Default value : 1.0f </para>
        /// </summary>
        public float Spacing { get; set; } = 1.0f;
    }
}
