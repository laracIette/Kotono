using Kotono.Utils;

namespace Kotono.Graphics.Objects.Settings
{
    internal class ImageSettings : Object2DSettings
    {
        /// <summary>
        /// The path to the Image's file.
        /// <para> Default value : "" </para>
        /// </summary>
        internal string Path { get; set; } = "";

        /// <summary>
        /// The color of the Image.
        /// <para> Default value : Color.White </para>
        /// </summary>
        internal Color Color { get; set; } = Color.White;
    }
}
