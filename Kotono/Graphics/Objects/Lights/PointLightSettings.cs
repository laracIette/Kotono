﻿using Kotono.Utils;

namespace Kotono.Graphics.Objects.Lights
{
    /// <summary>
    /// Settings class for creating a <see cref="PointLight"/>.
    /// </summary>
    internal class PointLightSettings : LightSettings
    {
        /// <summary>
        /// The ambient color of the <see cref="PointLight"/>.
        /// </summary>
        /// <remarks> 
        /// Default value : Color.White 
        /// </remarks>
        public Color Ambient { get; set; } = Color.White;

        /// <summary>
        /// The specular color of the <see cref="PointLight"/>.
        /// </summary>
        /// <remarks> 
        /// Default value : Color.White 
        /// </remarks>
        public Color Specular { get; set; } = Color.White;

        /// <summary>
        /// The constant value of the <see cref="PointLight"/>.
        /// </summary>
        /// <remarks> 
        /// Default value : 1.0f 
        /// </remarks>
        public float Constant { get; set; } = 1.0f;

        /// <summary>
        /// The linear value of the <see cref="PointLight"/>.
        /// </summary>
        /// <remarks> 
        /// Default value : 0.09f 
        /// </remarks>
        public float Linear { get; set; } = 0.09f;

        /// <summary>
        /// The quadratic value of the <see cref="PointLight"/>.
        /// </summary>
        /// <remarks> 
        /// Default value : 0.032f 
        /// </remarks>
        public float Quadratic { get; set; } = 0.032f;
    }
}
