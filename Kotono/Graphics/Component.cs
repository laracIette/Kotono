using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics
{
    public class Component
    {
        private readonly Viewport _viewport;

        private readonly RoundedBox _background;
        
        public Component(Rect dest) 
        { 
            _viewport = new Viewport(dest);

            _background = new RoundedBox(dest, Color.White, 0, 3, 10);
        }

        public void Update()
        {
        }

        public void UpdateShaders()
        {
        }

        public void Draw()
        {
            _viewport.Use();
        }
    }
}
