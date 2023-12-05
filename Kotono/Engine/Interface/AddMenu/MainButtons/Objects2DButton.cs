using Kotono.Utils;

namespace Kotono.Engine.Interface.AddMenu.MainButtons
{
    public class Objects2DButton : MainButton
    {
        public Objects2DButton()
            : base("2D Objects", new string[] { "Image", "Text", "Rounded Box", "Rounded Border" }, Anchor.BottomRight)
        { }
    }
}
