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

        internal WindowComponent(Rect Rect, Color color)
        {
            Viewport = new Viewport(new Object2DSettings { Rect = Rect });
            _background = new Background(Rect.FromAnchor(new Rect(Point.Zero, Rect.Size), Anchor.TopLeft), color, Viewport);
        }

        internal void Update()
        {

        }
    }
}
