using Kotono.Utils;
using System;
using System.Linq;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Print
{
    internal static class Printer
    {
        private static readonly PrinterText[] _texts;

        private static int _currentIndex = 0;

        static Printer()
        {
            _texts = new PrinterText[50];

            for (int i = 0; i < _texts.Length; i++)
            {
                _texts[i] = new PrinterText();
            }
        }

        internal static void Print(string? text, Color color)
        {
            if (text != null)
            {
                // Split the text for each line skip, and Reverse the list cause the last element printed gets lowered, so it's by default in wrong order
                foreach (var token in text.Split('\n').Reverse())
                {
                    Lower();
                    _texts[_currentIndex].SetText(token);
                    _texts[_currentIndex].Color = color;

                    _currentIndex = (int)Math.Loop(_currentIndex + 1, _texts.Length);
                }
            }
        }

        private static void Lower()
        {
            foreach (var text in _texts)
            {
                text.Lower();
            }
        }
    }
}
