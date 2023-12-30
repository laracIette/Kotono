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
            _background = new Background(Rect.FromAnchor(new Rect(Point.Zero, dest.Size), Anchor.TopLeft), Viewport);
        }

        public void Update()
        {
            _background.Position += Point.Unit * 20.0f * Time.DeltaS;
        }
    }
}
