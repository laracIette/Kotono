using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal static class ComponentManager
    {
        private static readonly List<Component> _components =
        [
            new Component(new Rect(0.0f, 0.0f, 1280.0f, 720.0f)),
            new Component(new Rect(100.0f, 100.0f, 640.0f, 360.0f))
        ];

        internal static Component Window => _components[0];

        internal static Viewport ActiveViewport { get; set; } = _components[0].Viewport;

        internal static void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }
    }
}
