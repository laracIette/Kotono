using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;
using System;
using System.Linq;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics
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

        private static void Lower()
        {
            foreach (var text in _texts)
            {
                text.Lower();
            }
        }

        private static void Print(string? text, Color color)
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

        /// <summary>
        /// Prints an object to the Window given a Color.
        /// </summary>
        /// <param name="obj"> The object to print. </param>
        /// <param name="color"> The Color of the text. </param>
        internal static void Print(object? obj, Color color)
        {
            if (obj != null)
            {
                Print(obj.ToString(), color);
            }
        }

        /// <summary>
        /// Prints an object to the Window.
        /// </summary>
        /// <param name="obj"> The object to print. </param>
        /// <param name="rainbow"> A bool to determine whether the Color of the text should loop through RBG values. </param>
        internal static void Print(object? obj, bool rainbow = false)
        {
            Print(obj, rainbow ? Color.Rainbow(0.01f) : Color.White);
        }

        /// <summary>
        /// Prints an empty line.
        /// </summary>
        internal static void Print()
        {
            Print("");
        }
    }
}
