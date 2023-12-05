using Kotono.Utils;

namespace Kotono.Engine.Interface.AddMenu.MainButtons
{
    public class LightsButton : MainButton
    {
        public LightsButton()
            : base("Lights", new string[] { "PointLight", "SpotLight", "Directional Light" }, Anchor.TopRight)
        { }
    }
}
