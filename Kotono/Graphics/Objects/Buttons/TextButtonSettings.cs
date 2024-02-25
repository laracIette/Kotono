using Kotono.Graphics.Objects.Texts;

namespace Kotono.Graphics.Objects.Buttons
{
    /// <summary>
    /// Settings class for creating a <see cref="TextButton"/>.
    /// </summary>
    internal class TextButtonSettings : ButtonSettings
    {
        /// <summary>
        /// The text that the TextButton should display.
        /// </summary>
        /// <remarks> 
        /// Default value : <see langword="new"/> <see cref="Texts.TextSettings"/>()
        /// </remarks>
        public TextSettings TextSettings { get; set; } = new();
    }
}
