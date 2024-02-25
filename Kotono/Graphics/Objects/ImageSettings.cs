namespace Kotono.Graphics.Objects
{
    /// <summary>
    /// Settings class for creating an <see cref="Image"/>.
    /// </summary>
    internal class ImageSettings : Object2DSettings
    {
        /// <summary>
        /// The path to the texture of the Image.
        /// </summary>
        /// <remarks> 
        /// Default value : "" 
        /// </remarks>
        public string Texture { get; set; } = "";
    }
}
