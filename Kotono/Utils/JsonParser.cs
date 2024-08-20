using Kotono.Graphics.Objects;
using System;
using System.Text.Json;
using IO = System.IO;

namespace Kotono.Utils
{
    internal class JsonParser : Object
    {
        private JsonParser() { }

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = true,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            IncludeFields = true
        };

        internal static T Parse<T>(string path) where T : Drawable
        {
            string jsonString = IO.File.ReadAllText(path);

            var settings = JsonSerializer.Deserialize<T>(jsonString) ?? Activator.CreateInstance<T>();

            return settings;
        }

        internal static void WriteFile(Drawable settings, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string jsonString = JsonSerializer.Serialize(settings, settings.GetType(), _jsonSerializerOptions);
                IO.File.WriteAllText(path, jsonString);
            }
        }
    }
}
