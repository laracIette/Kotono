﻿using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;
using System;
using System.Diagnostics;
using System.Linq;

namespace Kotono.Graphics
{
    internal static class Printer
    {
        private static readonly PrinterText[] _texts =
            Enumerable.Range(0, 50)
            .Select(i => new PrinterText())
            .ToArray();

        private static int _currentIndex = 0;

        private static int CurrentIndex
        {
            get => _currentIndex;
            set => _currentIndex = Math.Loop(value, _texts.Length);
        }

        private static void Lower()
        {
            foreach (var text in _texts)
            {
                text.Lower();
            }
        }

        /// <summary>
        /// Prints an text to the <see cref="Window"/> given a <see cref="Color"/>.
        /// </summary>
        /// <param name="text"> The text to print. </param>
        /// <param name="color"> The color of the text. </param>
        [Conditional("DEBUG")]
        internal static void Print(string? text, Color color)
        {
            if (text is not null)
            {
                // Split the text for each line skip,
                // and Reverse the list cause the last element printed gets lowered,
                // so it's by default in wrong order
                foreach (var token in text.Split('\n').Reverse())
                {
                    ++CurrentIndex;

                    Lower();

                    _texts[CurrentIndex].IsDraw = true;
                    _texts[CurrentIndex].Value = token;
                    _texts[CurrentIndex].LettersColor = color;
                }
            }
        }

        /// <summary>
        /// Prints an empty line to the <see cref="Window"/>.
        /// </summary>
        [Conditional("DEBUG")]
        internal static void Print()
            => Print(string.Empty, Color.White);

        /// <summary>
        /// Prints an object to the <see cref="Window"/> given a <see cref="Color"/>.
        /// </summary>
        /// <param name="obj"> The object to print. </param>
        /// <param name="color"> The color of the text. </param>
        [Conditional("DEBUG")]
        internal static void Print(object? obj, Color color)
            => Print(obj?.ToString(), color);

        /// <summary>
        /// Prints objects to the <see cref="Window"/>, each separated by a whitespace.
        /// </summary>
        /// <param name="objs"> The objects to log. </param>
        [Conditional("DEBUG")]
        internal static void Print(params object?[] objs)
            => Print(string.Join(' ', objs), Color.White);

        /// <summary>
        /// Prints an object to the <see cref="Window"/>.
        /// </summary>
        /// <param name="obj"> The object to print. </param>
        /// <param name="frequency"> The frequency at which the <see cref="Color"/> loops through RGB values. </param>
        [Conditional("DEBUG")]
        internal static void PrintRainbow(object? obj, float frequency)
            => Print(obj, Color.Rainbow(frequency));
    }
}
