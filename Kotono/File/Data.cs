using System.Collections.Generic;

namespace Kotono.File
{
    public class Data
    {
        public Dictionary<string, string> Strings { get; } = new();

        public Dictionary<string, float> Floats { get; } = new();

        public Dictionary<string, double> Doubles { get; } = new();

        public Dictionary<string, int> Ints { get; } = new();

        public Data() { }
        
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
