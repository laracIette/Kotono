using Kotono.Utils;

namespace Kotono.File
{
    internal class ShapeSettings : Object3DSettings
    {
        /// <summary>
        /// The vertices of the Shape.
        /// <para> Default value : [] </para>
        /// </summary>
        public Vector[] Vertices { get; set; } = [];
    }
}
