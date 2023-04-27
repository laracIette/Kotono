using Kotono.Utils;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Print
{
    public class Printer
    {
        private readonly List<Text> _texts = new();

        public Printer() { }

        public void Update()
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

        public void Print(string text)
        {
            foreach (var _text in _texts)
            {
                _text.Lower();
            }
            _texts.Add(new Text(text));
        }
    }
}
