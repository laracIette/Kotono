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
            get => _background.Position;
            set
            {
                _background.Position = value;
                Viewport.Position = Rect.GetPositionFromAnchor(value, Size, Anchor.BottomRight);
            }
        }

        internal Point Size
        {
            get => _background.Size;
            set
            {
                _background.Size = value;
                Viewport.Size = value;
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
