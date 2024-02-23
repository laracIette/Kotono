using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal static class ComponentManager
    {
        private static readonly List<Component> _components =
        [
            new Component(new Rect(0.0f, 0.0f, 1280.0f, 720.0f), Color.Transparent)
        ];

        internal static Viewport WindowViewport => _components[0].Viewport;

        internal static Viewport ActiveViewport { get; set; } = WindowViewport;

        internal static void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }
    }
}
