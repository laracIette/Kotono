using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal static class WindowComponentManager
    {
        private static readonly List<WindowComponent> _components =
        [
            new WindowComponent(new Rect(0.0f, 0.0f, 1280.0f, 720.0f), Color.Transparent)
        ];

        internal static Viewport WindowViewport => _components[0].Viewport;

        internal static Viewport ActiveViewport { get; set; } = WindowViewport;
    }
}
