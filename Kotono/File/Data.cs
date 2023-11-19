using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.File
{
    public class Data
    {
        public Dictionary<string, string> Strings { get; } = new();

        public Dictionary<string, float> Floats { get; } = new();

        public Dictionary<string, double> Doubles { get; } = new();

        public Dictionary<string, int> Ints { get; } = new();

        public List<string> Keys => Strings.Keys
            .Concat(Floats.Keys)
            .Concat(Doubles.Keys)
            .Concat(Ints.Keys)
            .ToList();

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
                result += $"{key}: {value:F}f\n";
            }
            foreach ((var key, var value) in Doubles)
            {
                result += $"{key}: {value:F}\n";
            }
            foreach ((var key, var value) in Ints)
            {
                result += $"{key}: {value}\n";
            }

            return result;
        }

        public object GetValue(string key)
        {
            if (Strings.TryGetValue(key, out string? str))
            {
                return str;
            }
            if (Floats.TryGetValue(key, out float f))
            {
                return f;
            }
            if (Doubles.TryGetValue(key, out double d))
            {
                return d;
            }
            if (Ints.TryGetValue(key, out int i))
            {
                return i;
            }

            throw new Exception($"Value for key: \"{key}\" not found");
        }
    }
}
