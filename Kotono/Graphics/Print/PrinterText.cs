using Kotono.Graphics.Objects;

namespace Kotono.Graphics.Print
{
    internal class PrinterText : Text
    {
        internal PrinterText(string text)
            : base(text, new Rect(0f, 0f, 25f, 30f), Location.TopLeft, 2 / 3f) 
        {
        }

        internal override void SetText(string text)
        {   
            _text = text;
            Clear();

            _dest = new Rect(0f, 0f, 25f, 30f);
            Init();
        }

        internal void Lower()
        {
            _dest.Y += _dest.H;

            foreach (var letter in _letters)
            {
                letter.Dest.Y = _dest.Y + _dest.H / 2;
            }
        }
    }
}
