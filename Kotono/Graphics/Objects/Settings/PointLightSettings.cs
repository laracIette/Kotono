using Kotono.Utils;

namespace Kotono.Graphics.Objects.Settings
{
    internal class PointLightSettings : Object3DSettings
    {
        /// <summary>
        /// The ambient color of the PointLight.
        /// <para> Default value : Color.White </para>
        /// </summary>
        internal Color Ambient { get; set; } = Color.White;

        /// <summary>
        /// The diffuse color of the PointLight.
        /// <para> Default value : Color.White </para>
        /// </summary>
        internal Color Diffuse { get; set; } = Color.White;

        /// <summary>
        /// The specular color of the PointLight.
        /// <para> Default value : Color.White </para>
        /// </summary>
        internal Color Specular { get; set; } = Color.White;

        /// <summary>
        /// The constant value of the PointLight.
        /// <para> Default value : 1.0f </para>
        /// </summary>
        internal float Constant { get; set; } = 1.0f;

        /// <summary>
        /// The linear value of the PointLight.
        /// <para> Default value : 0.09f </para>
        /// </summary>
        internal float Linear { get; set; } = 0.09f;

        /// <summary>
        /// The quadratic value of the PointLight.
        /// <para> Default value : 0.032f </para>
        /// </summary>
        internal float Quadratic { get; set; } = 0.032f;
    }
}
