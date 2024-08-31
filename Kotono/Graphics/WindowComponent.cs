using Kotono.Engine.UserInterface.Elements;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics
{
    internal class WindowComponent : Object
    {
        private readonly Background _background;

        internal Viewport Viewport { get; } = new();

        internal Point Position
        {
            get => _background.RelativePosition;
            set
            {
                _background.RelativePosition = value;
                Viewport.RelativePosition = Rect.GetPositionFromAnchor(value, Size, Anchor.BottomRight);
            }
        }

        internal Point Size
        {
            get => _background.RelativeSize;
            set
            {
                _background.RelativeSize = value;
                Viewport.RelativeSize = value;
            }
        }

        internal Color BackgroundColor
        {
            get => _background.Color;
            set => _background.Color = value;
        }

        internal WindowComponent()
        {
            _background = new Background
            {
                Viewport = Viewport
            };
        }
    }
}
