using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics
{
    public class Component(Rect dest)
    {
        private readonly Viewport _viewport = new(dest);

        private readonly RoundedBox _background = new(dest, Color.White, 0, 3, 10);

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
