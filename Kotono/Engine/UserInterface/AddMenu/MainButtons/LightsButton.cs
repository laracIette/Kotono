using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    internal class LightsButton : MainButton
    {
        internal LightsButton()
            : base("Lights", ["PointLight", "SpotLight", "Directional Light"], Anchor.TopRight)
        { }
    }
}
