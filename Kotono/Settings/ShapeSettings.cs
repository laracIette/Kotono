using Kotono.Utils;
using Kotono.Graphics.Objects.Shapes;

namespace Kotono.Settings
{
    /// <summary>
    /// Settings class for creating a <see cref="Shape"/>.
    /// </summary>
    internal class ShapeSettings : Object3DSettings
    {
        /// <summary>
        /// The vertices of the Shape.
        /// <para> Default value : [] </para>
        /// </summary>
        public Vector[] Vertices { get; set; } = [];
    }
}
