using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    internal class LightsButton : MainButton
    {
        internal LightsButton()
            : base(["PointLight", "SpotLight", "Directional Light"], Anchor.TopRight)
        {
            Source = "Lights";
        }
    }
}
