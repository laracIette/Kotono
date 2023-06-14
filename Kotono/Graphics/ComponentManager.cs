using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class ComponentManager
    {
        private readonly List<Component> _components = new();

        internal ComponentManager() 
        {
        }

        internal void Init()
        {
            //_components.Add(new Component(new Rect(200, 180, 200, 300)));
        }

        internal void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }

        internal void UpdateShaders()
        {
            foreach (var component in _components)
            {
                component.UpdateShaders();
            }
        }

        internal void Draw()
        {
            foreach (var component in _components) 
            { 
                component.Draw();
            }
        }
    }
}
