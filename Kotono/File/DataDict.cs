using System;
using System.Collections.Generic;

namespace Kotono.File
{
    internal class DataDict<T>
    {
        private readonly Dictionary<string, T> _dict = new();

        internal Dictionary<string, T>.KeyCollection Keys => _dict.Keys;

        internal Dictionary<string, T>.ValueCollection Values => _dict.Values;

        internal DataDict() { }

        internal T this[string key]
        {
            get
            {
                _dict.TryGetValue(key, out T? value);
                return value ?? throw new Exception($"error: Dictionary doesn't contain the key \"{key}\"");
            }
            set => _dict[key] = value;
        }
    }
}
