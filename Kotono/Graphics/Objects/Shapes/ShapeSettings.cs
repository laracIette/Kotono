using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Shapes
{
    /// <summary>
    /// Settings class for creating a <see cref="Shape"/>.
    /// </summary>
    internal class ShapeSettings : Object3DSettings
    {
        /// <summary>
        /// The vertices of the Shape.
        /// </summary>
        /// <remarks> 
        /// Default value : [] 
        /// </remarks>
        public Vector[] Vertices { get; set; } = [];
    }
}
