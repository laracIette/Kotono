using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    internal sealed class LightsButton : MainButton
    {
        internal LightsButton()
            : base(["PointLight", "SpotLight", "Directional Light"])
        {
            Text.Source = "Lights";
            Anchor = Anchor.TopRight;
        }
    }
}
