using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    internal sealed class Objects3DButton : MainButton
    {
        internal Objects3DButton()
            : base(["Mesh"])
        {
            Text.Value = "3D Objects";
            Anchor = Anchor.BottomLeft;
        }
    }
}
