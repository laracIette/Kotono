using Kotono.Graphics.Shaders;
using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    /// <summary>
    /// An object that can be drawn.
    /// </summary>
    internal interface IDrawable : IObject
    {
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
        /// The viewport in which the <see cref="IDrawable"/> is drawn.
        /// </summary>
        public Viewport Viewport { get; set; }

        /// <summary>
        /// The shader the <see cref="IDrawable"/> uses to be drawn.
        /// </summary>
        public Shader Shader { get; }

        /// <summary>
        /// The objects that are dependant to the <see cref="IDrawable"/>.
        /// </summary>
        public List<Drawable> Childrens { get; }

        /// <summary>
        /// Draw the <see cref="IDrawable"/>.
        /// </summary>
        public void Draw();
    }
}
