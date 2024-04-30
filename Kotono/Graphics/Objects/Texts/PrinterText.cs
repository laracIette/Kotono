using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Timing;

namespace Kotono.Graphics.Objects.Texts
{
    internal class PrinterText : Text
    {
        private readonly Timer _clear = new();

        //public override bool IsUpdate => IsDraw;

        internal PrinterText()
            : base(
                new TextSettings
                {
                    Dest = new Rect(Point.Zero, 25.0f, 30.0f),
                    Anchor = Anchor.TopLeft,
                    Spacing = 2.0f / 3.0f,
                    Layer = int.MaxValue
                }
            )
        {
            _clear.Timeout += OnClearTimeout;
        }

        internal override void SetText(string text)
        {
            _text = text;

            Position = Point.Zero;
            Init();

            _clear.Start(3.0f);
        }

        internal void Lower()
        {
            _lettersDest.Y += _lettersDest.H;

            foreach (var letter in _letters)
            {
                letter.Y = _lettersDest.Y + _lettersDest.H / 2.0f;
            }
        }

        private void OnClearTimeout(object? sender, TimedEventArgs e)
        {
            Clear();
        }
    }
}
