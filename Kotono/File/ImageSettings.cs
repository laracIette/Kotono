using Kotono.Graphics.Objects;

namespace Kotono.File
{
    /// <summary>
    /// Settings class for creating an <see cref="Image"/>.
    /// </summary>
    internal class ImageSettings : Object2DSettings
    {
        /// <summary>
        /// The path to the texture of the Image.
        /// <para> Default value : "" </para>
        /// </summary>
        public string Texture { get; set; } = "";
    }
}
