using Kotono.Graphics.Objects;

namespace Kotono.Engine.UserInterface.Elements
{
    internal class Background : RoundedBox
    {
        public Background()
        {
            FallOff = 1.0f;
            CornerSize = 15.0f;
        }
    }
}
