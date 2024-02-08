using Kotono.Utils;
using Kotono.Graphics.Objects.Lights;

namespace Kotono.Settings
{
    /// <summary>
    /// Settings class for creating a <see cref="PointLight"/>.
    /// </summary>
    internal class PointLightSettings : Object3DSettings
    {
        /// <summary>
        /// The ambient color of the PointLight.
        /// <para> Default value : Color.White </para>
        /// </summary>
        public Color Ambient { get; set; } = Color.White;

        /// <summary>
        /// The specular color of the PointLight.
        /// <para> Default value : Color.White </para>
        /// </summary>
        public Color Specular { get; set; } = Color.White;

        /// <summary>
        /// The constant value of the PointLight.
        /// <para> Default value : 1.0f </para>
        /// </summary>
        public float Constant { get; set; } = 1.0f;

        /// <summary>
        /// The linear value of the PointLight.
        /// <para> Default value : 0.09f </para>
        /// </summary>
        public float Linear { get; set; } = 0.09f;

        /// <summary>
        /// The quadratic value of the PointLight.
        /// <para> Default value : 0.032f </para>
        /// </summary>
        public float Quadratic { get; set; } = 0.032f;
    }
}
