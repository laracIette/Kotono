using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Texts
{
    internal sealed class PrinterText : Text
    {
        public override string Value
        {
            get => base.Value;
            set
            {
                RelativePosition = Point.Zero;

                base.Value = value;
                
                ExecuteAction.Delay(() => IsDraw = false, 3.0f);
            }
        }

        internal PrinterText()
        {
            LettersSize = new Point(25.0f, 30.0f);
            Anchor = Anchor.TopLeft;
            Spacing = 2.0f / 3.0f;
            Layer = int.MaxValue;
            IsDraw = false;
        }

        internal void Lower()
            => RelativePosition += new Point(0.0f, LettersSize.Y);
    }
}
