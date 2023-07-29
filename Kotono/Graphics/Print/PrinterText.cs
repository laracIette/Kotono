using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics.Print
{
    public class PrinterText : Text
    {
        public PrinterText(string text)
            : base(text, new Rect(0f, 0f, 25f, 30f), Anchor.TopLeft, Color.White, 2 / 3f, int.MaxValue) 
        {
        }

        public override void SetText(string text)
        {   
            _text = text;
            Clear();

            _lettersDest = new Rect(0f, 0f, 25f, 30f);
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
