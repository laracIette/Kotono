using System;

namespace Kotono
{
    internal static class Logger
    {
        /// <summary>
        /// Writes an object to the console.
        /// </summary>
        /// <param name="obj"> The object to log. </param>
        internal static void Log(object? obj)
        {
            Console.WriteLine(obj);
        }

        /// <summary>
        /// Writes an empty line to the console.
        /// </summary>
        internal static void Log()
        {
            Log("");
        }
    }
}
