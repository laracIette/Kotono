using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    /// <summary>
    /// Settings class for creating an <see cref="Object2D"/>.
    /// </summary>
    internal class Object2DSettings : DrawableSettings
    {
        private Rect _rect = Rect.Default;

        /// <summary>
        /// The Rect of the Object2D.
        /// </summary>
        /// <remarks> 
        /// Default value : Rect.Zero 
        /// </remarks>
        public Rect Rect 
        {
            get => _rect;
            set
            {
                if (_rect != value)
                {
                    _rect.Dispose();
                    _rect = value;
                }
            }
        }

        /// <summary>
        /// The layer of the Object2D.
        /// </summary>
        /// <remarks> 
        /// Default value : 0 
        /// </remarks>
        public int Layer { get; set; } = 0;
    }
}
