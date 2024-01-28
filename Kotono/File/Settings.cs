using System.Text.Json;
using IO = System.IO;

namespace Kotono.File
{
    internal static class Settings
    {
        public static readonly JsonSerializerOptions _jsonSerializerOptions = 
            new() 
            { 
                WriteIndented = true, 
                IncludeFields = true,
            };

        internal static T Parse<T>(string path) where T : DrawableSettings
        {
            string jsonString = IO.File.ReadAllText(path);
            
            var settings = JsonSerializer.Deserialize<T>(jsonString) ?? throw new JsonException($"error: couldn't deserialize from file \"{path}\" to type \"{typeof(T)}\"");
            
            settings.Path = path;

            return settings;
        }

        internal static void WriteFile<T>(T settings) where T : DrawableSettings
        {
            string jsonString = JsonSerializer.Serialize(settings, settings.GetType(), _jsonSerializerOptions);
            IO.File.WriteAllText(settings.Path, jsonString);
        }
    }
}
