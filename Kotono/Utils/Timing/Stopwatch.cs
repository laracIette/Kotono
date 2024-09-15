using System;
using System.Runtime.CompilerServices;

namespace Kotono.Utils.Timing
{
    /// <summary>
    /// Class for exact time spans.
    /// </summary>
    internal sealed class Stopwatch
    {
        private double _startTime = Time.ExactUTC;

        /// <summary>
        /// Get the exact elapsed time since the Stopwatch started.
        /// </summary>        
        internal double ElapsedTime => Time.ExactUTC - _startTime;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Start() => _startTime = Time.ExactUTC;

        public override string ToString()
        {
            return $"Start Time: {_startTime}, Elapsed Time: {ElapsedTime}";
        }

        internal static double Measure(Action action)
        {
            var stopwatch = new Stopwatch();
            action();
            return stopwatch.ElapsedTime;
        }
    }
}
