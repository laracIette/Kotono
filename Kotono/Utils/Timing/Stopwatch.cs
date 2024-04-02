using System.Runtime.CompilerServices;

namespace Kotono.Utils.Timing
{
    /// <summary>
    /// Class for exact time spans.
    /// </summary>
    public class Stopwatch
    {
        private double _startTime = Time.ExactUTC;

        /// <summary>
        /// Get the exact elapsed time since the Stopwatch started.
        /// </summary>
        public double ElapsedTime => Time.ExactUTC - _startTime;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start() => _startTime = Time.ExactUTC;
    }
}
