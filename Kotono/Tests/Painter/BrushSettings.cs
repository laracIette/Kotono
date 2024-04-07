using Kotono.Utils.Coordinates;

namespace Kotono.Tests.Painter
{
    internal class BrushSettings
    {
        /// <summary>
        /// The name of the <see cref="Brush"/>.
        /// </summary>
        /// <remarks>
        /// Default value : ""
        /// </remarks>
        public string Name { get; set; } = "";

        /// <summary>
        /// The size of the <see cref="Brush"/>.
        /// </summary>
        /// <remarks>
        /// Default value : Point.Zero
        /// </remarks>
        public Point Size { get; set; } = Point.Zero;
    }
}
