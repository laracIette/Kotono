using System;
using System.Runtime.CompilerServices;

namespace Kotono
{
    public static class Logger
    {
        /// <summary>
        /// Prints an object to the console.
        /// </summary>
        /// <param name="obj"> The object to log. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(object? obj)
        {
            Console.WriteLine(obj);
        }

        /// <summary>
        /// Prints an empty line to the console.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log()
        {
            Log("");
        }
    }
}
