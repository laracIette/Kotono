using System.Diagnostics;

namespace Kotono
{
    public static class Logger
    {
        /// <summary>
        /// Prints an object to the console.
        /// </summary>
        /// <param name="obj"> The object to log. </param>
        public static void Log(object? obj) => Debug.WriteLine(obj);

        /// <summary>
        /// Prints an empty line to the console.
        /// </summary>
        public static void Log() => Log(string.Empty);

        /// <summary>
        /// Prints an error to the console.
        /// </summary>
        public static void LogError(string err) => Log($"error: {err}.");
    }
}
