﻿using Kotono.Tests;
using System.Collections.Generic;

namespace Kotono.File
{
    internal class Data
    {
        internal Dictionary<string, string> Strings { get; } = new();

        internal Dictionary<string, float> Floats { get; } = new();

        internal Dictionary<string, double> Doubles { get; } = new();

        internal Dictionary<string, int> Ints { get; } = new();

        internal Dictionary<string, MultiType> KeyValues { get; } = new();

        internal Data() { }
        
        public override string ToString()
        {
            string result = "";

            foreach (var pair in Strings)
            {
                result += $"{pair.Key}: {pair.Value}\n";
            }
            foreach (var pair in Floats)
            {
                result += $"{pair.Key}: {pair.Value}f\n";
            }
            foreach (var pair in Doubles)
            {
                result += $"{pair.Key}: {pair.Value}\n";
            }
            foreach (var pair in Ints)
            {
                result += $"{pair.Key}: {pair.Value}\n";
            }

            return result;
        }

    }
}
