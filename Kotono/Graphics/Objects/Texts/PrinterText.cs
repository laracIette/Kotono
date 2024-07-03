using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Timing;

namespace Kotono.Graphics.Objects.Texts
{
    internal class PrinterText() 
        : Text(
            new TextSettings
            {
                Rect = new Rect(Point.Zero, new Point(25.0f, 30.0f)),
                Anchor = Anchor.TopLeft,
                Spacing = 2.0f / 3.0f,
                Layer = int.MaxValue
            }
        )
    {
        internal override object? Source 
        { 
            get => base.Source; 
            set
            {
                Position = Point.Zero;

                base.Source = value;

                ExecuteAction.Delay(Clear, 3.0f);
            }
        }

        internal void Lower()
        {
            _lettersRect.Position += new Point(0.0f, _lettersRect.BaseSize.Y);

            foreach (var letter in _letters)
            {
                letter.Position += new Point(0.0f, _lettersRect.BaseSize.Y);
            }
        }
    }
}
