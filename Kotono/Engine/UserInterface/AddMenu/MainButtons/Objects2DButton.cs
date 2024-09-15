using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    internal sealed class Objects2DButton : MainButton
    {
        internal Objects2DButton()
            : base(["Image", "Text", "Rounded Box", "Rounded Border"])
        {
            Text.Source = "2D Objects";
            Anchor = Anchor.BottomRight;
        }
    }
}
