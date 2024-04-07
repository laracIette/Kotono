using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Shapes
{
    /// <summary>
    /// Settings class for creating a <see cref="Shape"/>.
    /// </summary>
    internal class Shape2DSettings : Object2DSettings
    {
        /// <summary>
        /// The points of the <see cref="Shape2D"/>.
        /// </summary>
        /// <remarks> 
        /// Default value : [] 
        /// </remarks>
        public Point[] Points { get; set; } = [];

        /// <summary>
        /// Whether the render of the <see cref="Shape2D"/> should loop back to first vertex.
        /// </summary>
        /// <remarks>
        /// Default value : true
        /// </remarks>
        public bool IsLoop { get; set; } = true;
    }
}
