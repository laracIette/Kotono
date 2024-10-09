using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Texts;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using IO = System.IO;

namespace Kotono.Utils
{
    internal static class JsonParser
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = true,
            IncludeFields = true,
            IgnoreReadOnlyProperties = true,
            IgnoreReadOnlyFields = false,
        };

        internal static void Init()
        {
            Logger.Log("parsing saveables");
            foreach (var path in Directory.GetFiles(Path.DATA))
            {
                Logger.Log("found", path);
                if (TryParseFile<ISaveable>(path, out var saveable))
                {
                    Logger.Log("created", saveable);
                    ObjectManager.Create(saveable);
                }
            }
        }

        internal static bool TryParseFile<T>(string path, [NotNullWhen(true)] out T? obj)
        {
            string jsonString = File.ReadAllText(path);
            obj = JsonSerializer.Deserialize<T>(jsonString);
            return obj is not null;
        }

        internal static bool TryWriteFile<T>(T obj, string path)
        {
            if (obj is Image or PrinterText)
            {
                return false;
            }

            if (path.Any(c => IO.Path.GetInvalidPathChars().Contains(c)))
            {
                Logger.LogError("couldn't write file, invalid chars in path:", path);
                return false;
            }

            string fileName = IO.Path.GetFileName(path);
            if (fileName.Any(c => IO.Path.GetInvalidFileNameChars().Contains(c)))
            {
                Logger.LogError("couldn't write file, invalid chars in fileName:", fileName);
                return false;
            }

            string jsonString = JsonSerializer.Serialize(obj, typeof(T), _jsonSerializerOptions);
            File.WriteAllText(path, jsonString);

            return true;
        }

        internal static async Task<bool> TryWriteFileAsync<T>(T obj, string path)
        {
            if (path.Any(c => IO.Path.GetInvalidPathChars().Contains(c)))
            {
                Logger.LogError("couldn't write file, invalid chars in path:", path);
                return false;
            }

            string fileName = IO.Path.GetFileName(path);
            if (fileName.Any(c => IO.Path.GetInvalidFileNameChars().Contains(c)))
            {
                Logger.LogError("couldn't write file, invalid chars in fileName:", fileName);
                return false;
            }

            try
            {
                await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
                await JsonSerializer.SerializeAsync(stream, obj, _jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                Logger.LogError("couldn't write file, exception occurred:", ex.Message);
                return false;
            }

            return true;
        }
    }
}
