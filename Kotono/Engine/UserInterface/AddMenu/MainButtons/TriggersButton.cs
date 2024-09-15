using Kotono.Utils;

namespace Kotono.Engine.UserInterface.AddMenu.MainButtons
{
    internal sealed class TriggersButton : MainButton
    {
        internal TriggersButton()
            : base([])
        {
            Text.Source = "Triggers";
            Anchor = Anchor.TopLeft;
        }
    }
}
