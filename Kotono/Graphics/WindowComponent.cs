using Kotono.Engine.UserInterface.Elements;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics
{
    internal class WindowComponent : Object
    {
        internal Viewport Viewport { get; }

        private readonly Background _background;

        internal WindowComponent(Rect rect, Color color)
        {
            Viewport = new Viewport(rect);

            var position = Rect.FromAnchor(Point.Zero, rect.BaseSize, Anchor.TopLeft);

            _background = new Background(new Rect(position, rect.BaseSize), color, Viewport);
        }
    }
}
