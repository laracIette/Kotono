using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Engine.UserInterface.Elements
{
    internal class Background(Rect dest, Color color, Viewport viewport)
        : RoundedBox(dest, color, 0, 1.0f, 15.0f),
        IElement
    {
        public Viewport Viewport => viewport;
    }
}
