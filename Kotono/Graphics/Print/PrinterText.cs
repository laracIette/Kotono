using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics.Print
{
    public class PrinterText : Text
    {
        public PrinterText(string text)
            : base(text, new Rect(0f, 0f, 25f, 30f), Position.TopLeft, Color.White, 2 / 3f) 
        {
        }

        public override void SetText(string text)
        {   
            _text = text;
            Clear();

            _dest = new Rect(0f, 0f, 25f, 30f);
            Init();
        }

        public void Lower()
        {
            _dest.Y += _dest.H;

            foreach (var letter in _letters)
            {
                letter.Dest.Y = _dest.Y + _dest.H / 2;
            }
        }
    }
}
