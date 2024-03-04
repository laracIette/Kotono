using Kotono.Graphics.Objects.Buttons;

namespace Kotono.Settings
{
    /// <summary>
    /// Settings class for creating a <see cref="TextButtonList"/>.
    /// </summary>
    internal class TextButtonListSettings : TextButtonSettings
    {
        /// <summary>
        /// The texts of the TextButtonList.
        /// </summary>
        /// <remarks>
        /// Default value : []
        /// </remarks>
        public string[] Texts { get; set; } = [];
    }
}
