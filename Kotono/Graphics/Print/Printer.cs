using Kotono.Utils;
using System;
using System.Linq;

namespace Kotono.Graphics.Print
{
    internal class Printer
    {
        private readonly PrinterText[] _texts = new PrinterText[50];

        private int _currentIndex = 0;

        private int CurrentIndex
        {
            get
            {
                _currentIndex = (_currentIndex + 1) % _texts.Length;
                return _currentIndex;
            }
        }

        internal Printer() { }

        internal void Init()
        {
            for (int i = 0; i < _texts.Length; i++)
            {
                _texts[i] = new PrinterText("");
                _texts[i].Init();
            }
        }

        internal void Update()
        {
            foreach (var text in _texts)
            {
                if ((Time.NowS - text.Time) > 5f)
                {
                    text.Clear();
                }
            }
        }

        internal void Lower()
        {
            foreach (var text in _texts)
            {
                text.Lower();
            }
        }

        internal void Print(string? text)
        {
            if (text != null)
            {
                // Split the text for each line skip, and Reverse the list cause the last element printed gets lowered, so it's by default in wrong order
                foreach (var token in text.Split('\n').Reverse())
                {
                    Lower();
                    _texts[CurrentIndex].SetText(token);
                }
            }
        }
    }
}
