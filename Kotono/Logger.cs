using System;

namespace Kotono
{
    public static class Logger
    {
        /// <summary>
        /// Prints an object to the console.
        /// </summary>
        /// <param name="obj"> The object to log. </param>
        public static void Log(object? obj)
        {
            Console.WriteLine(obj);
        }

        /// <summary>
        /// Prints an empty line to the console.
        /// </summary>
        public static void Log()
        {
            Log("");
        }
    }
}
