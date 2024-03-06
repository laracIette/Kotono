using Kotono.Engine.UserInterface.Elements;
using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics
{
    internal class WindowComponent
    {
        internal Viewport Viewport { get; }

        private readonly Background _background;

        internal WindowComponent(Rect dest, Color color)
        {
            Viewport = new Viewport(new Object2DSettings { Dest = dest });
            _background = new Background(Rect.FromAnchor(new Rect(Point.Zero, dest.Size), Anchor.TopLeft), color, Viewport);
        }

        internal void Update()
        {

        }
    }
}
