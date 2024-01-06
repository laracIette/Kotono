using Kotono.Utils;

namespace Kotono.Graphics.Objects.Settings
{
    internal class TextSettings : Object2DSettings
    {
        /// <summary>
        /// The text that the Text should display.
        /// <para> Default value : "" </para>
        /// </summary>
        internal string Text { get; set; } = "";

        /// <summary>
        /// The anchor from which the Text should be drawn.
        /// <para> Default value : Anchor.Center </para>
        /// </summary>
        internal Anchor Anchor { get; set; } = Anchor.Center;

        /// <summary>
        /// The color of the Text.
        /// <para> Default value : Color.White </para>
        /// </summary>
        internal Color Color { get; set; } = Color.White;

        /// <summary>
        /// The spacing multiplier between each letter of the Text.
        /// <para> Default value : 1.0f </para>
        /// </summary>
        internal float Spacing { get; set; } = 1.0f;
    }
}
