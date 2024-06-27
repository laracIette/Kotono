using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Engine.UserInterface.Elements
{
    internal class Background : RoundedBox
    {
        public Background(Rect Rect, Color color, Viewport viewport)
            : base(new RoundedBoxSettings
            {
                Rect = Rect,
                Color = color,
                FallOff = 1.0f,
                CornerSize = 15.0f
            }
            )
        {
            Viewport = viewport;
        }
    }
}
