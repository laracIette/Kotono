using Kotono.File;
using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;

namespace Kotono.Graphics.Print
{
    internal class PrinterText()
        : Text(
            new TextSettings
            {
                Dest = new Rect(Point.Zero, 25.0f, 30.0f),
                Anchor = Anchor.TopLeft,
                Spacing = 2.0f / 3.0f,
                Layer = int.MaxValue
            }
        )
    {
        internal override void SetText(string text)
        {
            _text = text;

            Position = Point.Zero;
            Init();
        }

        internal void Lower()
        {
            _lettersDest.Y += _lettersDest.H;

            foreach (var letter in _letters)
            {
                letter.Y = _lettersDest.Y + _lettersDest.H / 2.0f;
            }
        }
    }
}
