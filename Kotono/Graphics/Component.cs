using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics
{
    public class Component(Rect dest)
    {
        public Viewport Viewport { get; } = new(dest);

        private readonly RoundedBox _background = new(Rect.FromAnchor(dest, Anchor.Center), Color.FromHex("#FFF1"), 0, 1, 10);

        public void Init()
        {
        }

        public void Update()
        {
        }

        public void UpdateShaders()
        {
        }

        public void Draw()
        {
            Viewport.Use();
            //_background.Draw();
        }
    }
}
