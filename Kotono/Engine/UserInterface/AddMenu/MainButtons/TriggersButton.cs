using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    internal sealed class TriggersButton : MainButton
    {
        internal TriggersButton()
            : base([])
        {
            Text.Value = "Triggers";
            Anchor = Anchor.TopLeft;
        }
    }
}
