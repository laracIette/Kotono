using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class ComponentManager
    {
        private readonly List<Component> _components = new();

        public ComponentManager() 
        {
        }

        public void Init()
        {
            //_components.Add(new Component(new Rect(600, 300, 300, 300)));
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
