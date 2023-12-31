using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Engine.UserInterface.Elements
{
    internal class Background(Rect dest, Viewport viewport)
#if false
        : RoundedBorder(dest, Color.FromHex("#FFF1"), 0, 1.0f, 15.0f, 8.0f),
#else
        : RoundedBox(dest, Color.FromHex("#FFF1"), 0, 1.0f, 15.0f),
#endif
        IElement
    {
        public Viewport Viewport => viewport;

        public override void Draw()
        {
            base.Draw();
        }
    }
}
