using Kotono.Graphics.Objects;

namespace Kotono.Engine.UserInterface.Elements
{
    internal sealed class Background : RoundedBox
    {
        internal Background()
        {
            TargetFallOff = 1.0f;
            TargetCornerSize = 15.0f;
        }
    }
}
