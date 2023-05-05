using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Print
{
    internal class Printer
    {
        private readonly List<Text> _texts = new();

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

        internal void Print(string text)
        {
            foreach (var _text in _texts)
            {
                _text.Lower();
            }
            _texts.Add(new Text(text));
        }
    }
}
