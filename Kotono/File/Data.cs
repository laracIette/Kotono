namespace Kotono.File
{
    internal class Data
    {
        public readonly DataDict<string> Strings = new();
        public readonly DataDict<float> Floats = new();
        public readonly DataDict<double> Doubles = new();
        public readonly DataDict<int> Ints = new();

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
