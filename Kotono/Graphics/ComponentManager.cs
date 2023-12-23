using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class ComponentManager
    {
        private readonly List<Component> _components = 
        [
            new Component(new Rect(0, 0, 1280, 720) / 2)
        ];

        public Viewport ActiveViewport => _components[0].Viewport;

        public ComponentManager() { }

        public void Init()
        {
            foreach (var component in _components)
            {
                component.Init();
            }
        }

        public void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }

        public void UpdateShaders()
        {
            foreach (var component in _components)
            {
                component.UpdateShaders();
            }
        }

        public void Draw()
        {
            foreach (var component in _components)
            {
                component.Draw();
            }
        }
    }
}
