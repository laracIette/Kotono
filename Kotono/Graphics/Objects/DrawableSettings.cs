using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    /// <summary>
    /// Settings class for creating a <see cref="Drawable"/>.
    /// </summary>
    internal class DrawableSettings : ObjectSettings
    {
        /// <summary>
        /// The path to the settings of the Drawable.
        /// </summary>
        /// <remarks> 
        /// Default value : "" 
        /// </remarks>
        public string Path { get; set; } = "";

        /// <summary>
        /// Wether the Drawable should be drawn.
        /// </summary>
        /// <remarks> 
        /// Default value : true 
        /// </remarks>
        public virtual bool IsDraw { get; set; } = true;

        /// <summary>
        /// The color of the Drawable.
        /// </summary>
        /// <remarks> 
        /// Default value : Color.White 
        /// </remarks>
        public Color Color { get; set; } = Color.White;
    }
}
