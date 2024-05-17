using Kotono.Engine.UserInterface.Elements;
using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics
{
    internal class WindowComponent : Object
    {
        internal Viewport Viewport { get; }

        private readonly Background _background;

        internal WindowComponent(Rect Rect, Color color)
        {
            Viewport = new Viewport(new Object2DSettings { Rect = Rect });

            var position = Rect.FromAnchor(Point.Zero, Rect.BaseSize, Anchor.TopLeft);

            _background = new Background(new Rect(position, Rect.BaseSize), color, Viewport);
        }
    }
}
