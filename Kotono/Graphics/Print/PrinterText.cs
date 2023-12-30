﻿using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics.Print
{
    public class PrinterText()
        : Text("", _dest, Anchor.TopLeft, Color.White, 2.0f / 3.0f, int.MaxValue)
    {
        private static readonly Rect _dest = new(0.0f, 0.0f, 25.0f, 30.0f);

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
                letter.Y = _lettersDest.Y + _lettersDest.H / 2.0f;
            }
        }
    }
}
