using Kotono.Utils;

namespace Kotono.Graphics.Objects.Settings
{
    internal class Object2DSettings : DrawableSettings
    {
        /// <summary>
        /// The dest of the Object2D.
        /// <para> Default value : Rect.Zero </para>
        /// </summary>
        [Parsable]
        public Rect Dest { get; set; } = Rect.Zero;

        /// <summary>
        /// The layer of the Object2D.
        /// <para> Default value : 0 </para>
        /// </summary>
        [Parsable]
        public int Layer { get; set; } = 0; 
    }
}
