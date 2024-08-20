using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    internal class Objects2DButton : MainButton
    {
        internal Objects2DButton()
            : base(["Image", "Text", "Rounded Box", "Rounded Border"], Anchor.BottomRight)
        {
            Source = "2D Objects";
        }
    }
}
