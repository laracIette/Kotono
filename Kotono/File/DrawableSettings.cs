using Kotono.Utils;

namespace Kotono.File
{
    /// <summary>
    /// Settings class for creating <see cref="Kotono.Graphics.Objects.Drawable"/>.
    /// </summary>
    internal class DrawableSettings
    {
        /// <summary>
        /// The path to the settings of the Drawable.
        /// <para> Default value : "" </para>
        /// </summary>
        public string Path { get; set; } = "";

        /// <summary>
        /// Wether the Drawable should be drawn.
        /// <para> Default value : true </para>
        /// </summary>
        public virtual bool IsDraw { get; set; } = true;

        /// <summary>
        /// The color of the Drawable.
        /// <para> Default value : Color.White </para>
        /// </summary>
        public Color Color { get; set; } = Color.White;
    }
}
