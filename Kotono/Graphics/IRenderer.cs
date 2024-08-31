using Kotono.Graphics.Objects;

namespace Kotono.Graphics
{
    internal interface IRenderer
    {
        /// <summary>
        /// Add an <see cref="IDrawable"/> to the <see cref="IRenderer"/>'s render queue.
        /// </summary>
        /// <param name="drawable"> The <see cref="IDrawable"/> to add. </param>
        public void AddToRenderQueue(IDrawable drawable);

        /// <summary>
        /// Draw each element of the <see cref="IRenderer"/>'s render queue.
        /// </summary>
        public void Render();
    }
}
