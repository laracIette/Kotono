using System.Collections.Generic;

namespace Kotono.File
{
    public class Data
    {
        public Dictionary<string, string> Dict { get; } = new();

        public Data() { }

        public override string ToString()
        {
            string result = "";

            foreach ((var key, var value) in Dict)
            {
                result += $"{key}: {value}\n";
            }

            return result;
        }

        public string this[string key]
        {
            get => Dict[key];
            set => Dict[key] = value;
        }
    }
}
