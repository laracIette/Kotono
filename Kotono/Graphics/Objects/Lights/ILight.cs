using Kotono.Utils;

namespace Kotono.Graphics.Objects.Lights
{
    internal interface ILight
    {
        /// <summary>
        /// Wether the <see cref="ILight"/> is emitting.
        /// </summary>
        public bool IsOn { get; set; }

        /// <summary>
        /// The intensity of the <see cref="ILight"/>.
        /// </summary>
        public float Intensity { get; set; }

        /// <summary>
        /// The ambient color of the <see cref="ILight"/>.
        /// </summary>
        public Color Ambient { get; set; }

        /// <summary>
        /// The diffuse color of the <see cref="ILight"/>.
        /// </summary>
        public Color Diffuse { get; set; }

        /// <summary>
        /// The specular color of the <see cref="ILight"/>.
        /// </summary>
        public Color Specular { get; set; }

        /// <summary>
        /// Switch the emission status of the <see cref="ILight"/>.
        /// </summary>
        public void Switch();
    }
}
