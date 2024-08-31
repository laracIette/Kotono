using Kotono.Graphics.Objects;

namespace Kotono.Engine.UserInterface.Elements
{
    internal class Background : RoundedBox
    {
        public Background()
        {
            TargetFallOff = 1.0f;
            TargetCornerSize = 15.0f;
        }
    }
}
