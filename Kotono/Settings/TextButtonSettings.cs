using Kotono.Graphics.Objects.Buttons;

namespace Kotono.Settings
{
    /// <summary>
    /// Settings class for creating a <see cref="TextButton"/>.
    /// </summary>
    internal class TextButtonSettings : ButtonSettings
    {
        /// <summary>
        /// The text that the TextButton should display.
        /// <para> Default value : "" </para>
        /// </summary>
        public string Text { get; set; } = "";
    }
}
