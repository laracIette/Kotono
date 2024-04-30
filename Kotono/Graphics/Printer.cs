using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;
using System;
using System.Linq;

namespace Kotono.Graphics
{
    internal static class Printer
    {
        private static readonly PrinterText[] _texts;

        private static int _currentIndex = 0;

        private static int CurrentIndex
        {
            get => _currentIndex;
            set => _currentIndex = (int)Math.Loop(value, _texts.Length);
        }

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
                    _texts[CurrentIndex].SetText(token);
                    _texts[CurrentIndex].Color = color;

                    CurrentIndex++;
                }
            }
        }

        /// <summary>
        /// Prints an object to the <see cref="Window"/> given a <see cref="Color"/>.
        /// </summary>
        /// <param name="obj"> The object to print. </param>
        /// <param name="color"> The color of the text. </param>
        internal static void Print(object? obj, Color color)
        {
            if (obj != null)
            {
                Print(obj.ToString(), color);
            }
        }

        /// <summary>
        /// Prints an object to the <see cref="Window"/>.
        /// </summary>
        /// <param name="obj"> The object to print. </param>
        /// <param name="isRainbow"> Whether the Color of the text should loop through RGB values. </param>
        internal static void Print(object? obj, bool isRainbow = false)
        {
            Print(obj, isRainbow ? Color.Rainbow(0.01f) : Color.White);
        }

        /// <summary>
        /// Prints an empty line to the <see cref="Window"/>.
        /// </summary>
        internal static void Print()
        {
            Print("");
        }
    }
}
