using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics
{
    public class Component(Rect dest) // issue : 0 is bottom left in viewport ? instead of top left
    {
        public Viewport Viewport { get; private set; } = new(dest);

        private readonly RoundedBox _background = new(Rect.FromAnchor(dest, Anchor.TopLeft), Color.FromHex("#FFF1"), 0, 1, 10);

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
