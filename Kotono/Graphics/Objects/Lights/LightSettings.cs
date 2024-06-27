namespace Kotono.Graphics.Objects.Lights
{
    /// <summary>
    /// Settings class for creating a <see cref="Light"/>.
    /// </summary>
    internal class LightSettings : Object3DSettings
    {
        /// <summary>
        /// Wether the <see cref="Light"/> is emitting.
        /// </summary>
        /// <remarks> 
        /// Default value : true 
        /// </remarks>
        public bool IsOn { get; set; } = true;

        /// <summary>
        /// The power of the <see cref="Light"/>.
        /// </summary>
        /// <remarks>
        /// Default value : 1.0f
        /// </remarks>
        public float Power { get; set; } = 1.0f;
    }
}
