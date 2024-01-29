using Kotono.Graphics.Objects;

namespace Kotono.Graphics
{
    internal interface IRenderer
    {
        public void AddToRenderQueue(IDrawable drawable);

        public void Render();
    }
}
