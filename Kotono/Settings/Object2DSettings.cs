using Kotono.Utils;
using Kotono.Graphics.Objects;

namespace Kotono.Settings
{
    /// <summary>
    /// Settings class for creating an <see cref="Object2D"/>.
    /// </summary>
    internal class Object2DSettings : DrawableSettings
    {
        /// <summary>
        /// The dest of the Object2D.
        /// </summary>
        /// <remarks> 
        /// Default value : Rect.Zero 
        /// </remarks>
        public Rect Dest { get; set; } = Rect.Zero;

        /// <summary>
        /// The layer of the Object2D.
        /// </summary>
        /// <remarks> 
        /// Default value : 0 
        /// </remarks>
        public int Layer { get; set; } = 0;
    }
}
