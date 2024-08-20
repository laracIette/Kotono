using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Texts
{
    internal class PrinterText : Text
    {
        public override object? Source
        {
            get => base.Source;
            set
            {
                Position = Point.Zero;

                base.Source = value;

                ExecuteAction.Delay(Clear, 3.0f);
            }
        }

        internal PrinterText()
        {
            Size = new Point(25.0f, 30.0f);
            Anchor = Anchor.TopLeft;
            Spacing = 2.0f / 3.0f;
            Layer = int.MaxValue;
        }

        internal void Lower()
        {
            LettersRect.Position += new Point(0.0f, LettersRect.BaseSize.Y);

            foreach (var letter in _letters)
            {
                letter.Position += new Point(0.0f, LettersRect.BaseSize.Y);
            }
        }
    }
}
