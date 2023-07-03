using System;

namespace Kotono.File
{
    internal class Data
    {
        internal DataDict<string> Strings { get; } = new();
        
        internal DataDict<float> Floats { get; } = new();
        
        internal DataDict<double> Doubles { get; } = new();
        
        internal DataDict<int> Ints { get; } = new();

        internal Data() { }

        public override string ToString()
        {
            string result = "";

            foreach (var key in Strings.Keys)
            {
                result += $"{key}: {Strings[key]}\n";
            }
            foreach (var key in Floats.Keys)
            {
                result += $"{key}: {Floats[key]}f\n";
            }
            foreach (var key in Doubles.Keys)
            {
                result += $"{key}: {Doubles[key]}\n";
            }
            foreach (var key in Ints.Keys)
            {
                result += $"{key}: {Ints[key]}\n";
            }

            return result;
        }
    }
}
