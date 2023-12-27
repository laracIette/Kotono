using Kotono.Engine.UserInterface.Elements;
using Kotono.Utils;

namespace Kotono.Graphics
{
    public class Component
    {
        public Viewport Viewport { get; }

        private readonly Background _background;

        public Component(Rect dest)
        {
            Viewport = new Viewport(dest);
            _background = new(Rect.FromAnchor(dest, Anchor.TopLeft), Viewport);
        }

        public void Init()
        {
        }

        public void Update()
        {
        }
    }
}
