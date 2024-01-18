using Kotono.Engine.UserInterface.Elements;
using Kotono.File;
using Kotono.Utils;

namespace Kotono.Graphics
{
    internal class Component
    {
        internal Viewport Viewport { get; }

        private readonly Background _background;

        internal Component(Rect dest, Color color)
        {
            Viewport = new Viewport(new Object2DSettings { Dest = dest });
            _background = new Background(Rect.FromAnchor(new Rect(Point.Zero, dest.Size), Anchor.TopLeft), color, Viewport);
        }

        internal void Update()
        {

        }
    }
}
