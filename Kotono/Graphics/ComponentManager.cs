using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal static class ComponentManager
    {
        private static readonly List<Component> _components =
        [
            new Component(new Rect(0, 0, 1280, 720)),
            new Component(new Rect(100, 100, 640, 360))
        ];

        internal static Component Window => _components[0];

        internal static Viewport ActiveViewport { get; set; } = _components[0].Viewport;

        internal static void Init()
        {
            foreach (var component in _components)
            {
                component.Init();
            }
        }

        internal static void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }
    }
}
