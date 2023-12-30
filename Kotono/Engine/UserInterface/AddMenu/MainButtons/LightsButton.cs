using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    public class LightsButton : MainButton
    {
        public LightsButton()
            : base("Lights", ["PointLight", "SpotLight", "Directional Light"], Anchor.TopRight)
        { }
    }
}
