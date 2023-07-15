using System.Collections.Generic;

namespace Kotono.File
{
    internal class Data
    {
        internal Dictionary<string, string> Strings { get; } = new();

        internal Dictionary<string, float> Floats { get; } = new();

        internal Dictionary<string, double> Doubles { get; } = new();

        internal Dictionary<string, int> Ints { get; } = new();

        internal Data() { }
        
        public override string ToString()
        {
            string result = "";

            foreach ((var key, var value) in Strings)
            {
                result += $"{key}: {value}\n";
            }
            foreach ((var key, var value) in Floats)
            {
                result += $"{key}: {value}f\n";
            }
            foreach ((var key, var value) in Doubles)
            {
                result += $"{key}: {value}\n";
            }
            foreach ((var key, var value) in Ints)
            {
                result += $"{key}: {value}\n";
            }

            return result;
        }

    }
}
