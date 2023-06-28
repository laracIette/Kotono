using Kotono.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Print
{
    internal class Printer
    {
        private readonly List<PrinterText> _texts = new();

        internal Printer() { }

        internal void Update()
        {
            for (int i = _texts.Count - 1; i >= 0; i--)
            {
                if (Time.NowS - _texts[i].Time > 5f)
                {
                    _texts[i].Clear();
                    _texts.RemoveAt(i);
                }
            }
        }

        internal void Lower()
        {
            foreach (var _text in _texts)
            {
                _text.Lower();
            }
        }

        internal void Print(string? text)
        {
            if (text != null)
            {
                foreach (var token in text.Split('\n'))
                {
                    Lower();
                    _texts.Add(new PrinterText(token));
                }
            }
        }
    }
}
