using Kotono.Utils;

namespace Kotono.Graphics.Objects.Settings
{
    internal class ShapeSettings : Object3DSettings
    {
        /// <summary>
        /// The vertices of the Shape.
        /// <para> Default value : [] </para>
        /// </summary>
        internal Vector[] Vertices { get; set; } = [];

        /// <summary>
        /// The color of the Shape.
        /// <para> Default value : Color.White </para>
        /// </summary>
        internal Color Color { get; set; } = Color.White;
    }
}
