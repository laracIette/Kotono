using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Texts
{
    internal sealed class PrinterText : Text
    {
        public override object? Source
        {
            get => base.Source;
            set
            {
                RelativePosition = Point.Zero;

                base.Source = value;

                ExecuteAction.Delay(Clear, 3.0f);
            }
        }

        internal PrinterText()
        {
            RelativeSize = new Point(25.0f, 30.0f);
            Anchor = Anchor.TopLeft;
            Spacing = 2.0f / 3.0f;
            Layer = int.MaxValue;
        }

        internal void Lower()
        {
            RelativePosition += new Point(0.0f, LettersRect.BaseSize.Y);
        }
    }
}
