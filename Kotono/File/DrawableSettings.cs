using Kotono.Utils;

namespace Kotono.File
{

    internal class DrawableSettings
    {
        /// <summary>
        /// The path to the settings of the Drawable.
        /// <para> Default value : "" </para>
        /// </summary>
        [Parsable]
        public string Path { get; set; } = "";

        /// <summary>
        /// Wether the Drawable should be drawn.
        /// <para> Default value : true </para>
        /// </summary>
        [Parsable]
        public virtual bool IsDraw { get; set; } = true;

        /// <summary>
        /// The color of the Drawable.
        /// <para> Default value : Color.White </para>
        /// </summary>
        [Parsable]
        public Color Color { get; set; } = Color.White;
    }
}
