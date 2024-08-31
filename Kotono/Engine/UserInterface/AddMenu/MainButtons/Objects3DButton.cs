using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    internal class Objects3DButton : MainButton
    {
        internal Objects3DButton()
            : base(["Mesh"], Anchor.BottomLeft)
        {
            Text.Source = "3D Objects";
        }
    }
}
