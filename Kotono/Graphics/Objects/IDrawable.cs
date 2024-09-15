using Kotono.Graphics.Shaders;
using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    /// <summary>
    /// An object that can be drawn.
    /// </summary>
    internal interface IDrawable : IObject
    {
        /// <summary>
        /// The name of the <see cref="IDrawable"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Wether the <see cref="IDrawable"/> should be drawn.
        /// </summary>
        public bool IsDraw { get; set; }

        /// <summary>
        /// The visibility of the <see cref="IDrawable"/>
        /// </summary>
        public Visibility Visibility { get; set; }

        /// <summary>
        /// The color of the <see cref="IDrawable"/>.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The shader the <see cref="IDrawable"/> uses to be drawn.
        /// </summary>
        public Shader Shader { get; }

        /// <summary>
        /// The viewport in which the <see cref="IDrawable"/> is drawn.
        /// </summary>
        public Viewport Viewport { get; set; }

        /// <summary>
        /// Update the <see cref="IDrawable"/>'s shader,
        /// is called immediately before drawing the <see cref="IDrawable"/>.
        /// </summary>
        public void UpdateShader();

        /// <summary>
        /// Draw the <see cref="IDrawable"/>,
        /// is called immediately after updating the <see cref="IDrawable"/>'s shader.
        /// </summary>
        public void Draw();
    }
}
