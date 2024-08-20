using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal static class WindowComponentManager
    {
        private static readonly List<WindowComponent> _windowComponents =
        [
            new WindowComponent
            {
                Size = new Point(1280.0f, 720.0f),
                Position = Rect.GetPositionFromAnchor(Point.Zero, new Point(1280.0f, 720.0f), Anchor.TopLeft),
                BackgroundColor = Color.Transparent
            }
        ];

        internal static Viewport WindowViewport => _windowComponents[0].Viewport;
    }
}
