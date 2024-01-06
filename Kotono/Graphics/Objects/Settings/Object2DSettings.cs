using Kotono.Utils;

namespace Kotono.Graphics.Objects.Settings
{
    internal class Object2DSettings : DrawableSettings
    {
        /// <summary>
        /// The dest of the Object2D.
        /// <para> Default value : Rect.Zero </para>
        /// </summary>
        internal Rect Dest { get; set; } = Rect.Zero;
        
        /// <summary>
        /// The layer of the Object2D.
        /// <para> Default value : 0 </para>
        /// </summary>
        internal int Layer { get; set; } = 0;
    }
}
