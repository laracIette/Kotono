using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics
{
    public class Component(Rect dest)
    {
        public Viewport Viewport { get; } = new(dest);

        private readonly RoundedBox _background = new(Rect.FromAnchor(new Rect(0, 0, 640, 360), Anchor.TopLeft), Color.FromHex("#FFF"), 0, 1, 30);

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
