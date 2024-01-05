using Kotono.Utils;
using System;
using System.Linq;

namespace Kotono.Graphics.Print
{
    internal static class Printer
    {
        private static readonly PrinterText[] _texts = Enumerable.Range(0, 50)
            .Select(_ => new PrinterText())
            .ToArray();

        private static int _currentIndex = 0;

        internal static void Update()
        {
            foreach (var text in _texts)
            {
                if ((Time.NowS - text.StartTime) > 3.0)
                {
                    text.Clear();
                }
            }
        }

        internal static void Lower()
        {
            foreach (var text in _texts)
            {
                text.Lower();
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

                    _currentIndex = (_currentIndex + 1) % _texts.Length;
                }
            }
        }
    }
}
