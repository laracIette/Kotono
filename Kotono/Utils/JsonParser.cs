using System;
using System.Text.Json;
using IO = System.IO;

namespace Kotono.Utils
{
    internal sealed class JsonParser
    {
        private JsonParser() { }

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = true,
            IgnoreReadOnlyProperties = true,
            IncludeFields = true
        };

        internal static T Parse<T>(string path)
        {
            string jsonString = IO.File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(jsonString) ?? Activator.CreateInstance<T>();
        }

        internal static void WriteFile(object obj, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string jsonString = JsonSerializer.Serialize(obj, obj.GetType(), _jsonSerializerOptions);
                IO.File.WriteAllText(path, jsonString);
            }
        }
    }
}
