using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;

namespace Kotono.Utils
{
    public static class Logger
    {
        /// <summary>
        /// Prints an object to the console.
        /// </summary>
        /// <param name="obj"> The object to log. </param>
        [Conditional("DEBUG")]
        public static void Log(object? obj) 
            => Debug.WriteLine(obj);

        /// <summary>
        /// Prints an empty line to the console.
        /// </summary>
        [Conditional("DEBUG")]
        public static void Log() 
            => Log(string.Empty);

        /// <summary>
        /// Prints objects to the console, each separated by a whitespace.
        /// </summary>
        /// <param name="objs"> The objects to log. </param>
        [Conditional("DEBUG")]
        public static void Log(params object?[] objs) 
            => Log(string.Join(' ', objs));

        /// <summary>
        /// Prints an error to the console.
        /// </summary>
        [Conditional("DEBUG")]
        public static void LogError(params object?[] objs) 
            => Log(["error:", .. objs]);

        /// <summary>
        /// Prints the last opengl error to the console.
        /// </summary>
        [Conditional("DEBUG")]
        public static void LogGLError(params object?[] objs) 
            => Log([GL.GetError(), .. objs]);
    }
}
