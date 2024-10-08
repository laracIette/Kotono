﻿using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Lights
{
    internal sealed class DirectionalLight : Light
    {
        /// <summary>
        /// The direction of the <see cref="DirectionalLight"/>.
        /// </summary>
        internal Vector Direction { get; set; }
    }
}
