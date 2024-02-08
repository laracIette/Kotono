using System;
using System.Text.Json;
using IO = System.IO;

namespace Kotono.Settings
{
    internal static class JsonParser
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

        internal static T Parse<T>(string path) where T : DrawableSettings
        {
            string jsonString = IO.File.ReadAllText(path);
            
            var settings = JsonSerializer.Deserialize<T>(jsonString) ?? Activator.CreateInstance<T>();
            
            settings.Path = path;

            return settings;
        }

        internal static void WriteFile<T>(T settings) where T : DrawableSettings
        {
            if (settings.Path != "")
            {
                string jsonString = JsonSerializer.Serialize(settings, settings.GetType(), _jsonSerializerOptions);
                IO.File.WriteAllText(settings.Path, jsonString);
            }
        }
    }
}
