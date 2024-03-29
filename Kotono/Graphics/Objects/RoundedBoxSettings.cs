﻿namespace Kotono.Graphics.Objects
{
    /// <summary>
    /// Settings class for creating a <see cref="RoundedBox"/>.
    /// </summary>
    internal class RoundedBoxSettings : Object2DSettings
    {
        /// <summary>
        /// The fall off of the RoundedBox.
        /// </summary>
        /// <remarks> 
        /// Default value : 1.0f 
        /// </remarks>
        public float FallOff { get; set; } = 1.0f;

        /// <summary>
        /// The corner size of the RoundedBox.
        /// </summary>
        /// <remarks> 
        /// Default value : 0.0f 
        /// </remarks>
        public float CornerSize { get; set; } = 0.0f;
    }
}
