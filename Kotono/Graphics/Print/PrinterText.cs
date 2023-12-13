using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics.Print
{
    public class PrinterText : Text
    {
        private static readonly Rect _dest = new(0f, 0f, 25f, 30f);

        public PrinterText()
            : base("", _dest, Anchor.TopLeft, Color.White, 2 / 3f, int.MaxValue)
        {
        }

        public override void SetText(string text)
        {
            _text = text;

            Position = _dest.Position;
            Init();
        }

        public void Lower()
        {
            _lettersDest.Y += _lettersDest.H;

            foreach (var letter in _letters)
            {
                letter.Y = _lettersDest.Y + _lettersDest.H / 2;
            }
        }
    }
}
