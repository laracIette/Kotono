﻿using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Shapes
{
    /// <summary>
    /// Settings class for creating a <see cref="Shape"/>.
    /// </summary>
    internal class ShapeSettings : Object3DSettings
    {
        /// <summary>
        /// The vertices of the <see cref="Shape"/>.
        /// </summary>
        /// <remarks> 
        /// Default value : [] 
        /// </remarks>
        public Vector[] Vertices { get; set; } = [];

        /// <summary>
        /// Whether the render of the <see cref="Shape"/> should loop back to first vertex.
        /// </summary>
        /// <remarks>
        /// Default value : true
        /// </remarks>
        public bool IsLoop { get; set; } = true;
    }
}
