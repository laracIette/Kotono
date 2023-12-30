using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Engine.UserInterface.Elements
{
    internal class Background(Rect dest, Viewport viewport)
        //: RoundedBorder(dest, Color.FromHex("#FFF1"), 0, 1.0f, 15.0f, 8.0f),
        : RoundedBox(dest, Color.FromHex("#FFF1"), 0, 1.0f, 15.0f),
        IElement
    {
        public Viewport Viewport => viewport;
    }
}
