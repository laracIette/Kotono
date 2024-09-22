namespace Kotono.Graphics.Objects.Lights
{
    internal interface IAttenuatedLight : ILight
    {
        /// <summary>
        /// The constant attenuation of the <see cref="IAttenuatedLight"/>.
        /// </summary>
        public float Constant { get; set; }

        /// <summary>
        /// The linear attenuation of the <see cref="IAttenuatedLight"/>.
        /// </summary>
        public float Linear { get; set; }

        /// <summary>
        /// The quadratic attenuation of the <see cref="IAttenuatedLight"/>.
        /// </summary>
        public float Quadratic { get; set; }
    }
}
