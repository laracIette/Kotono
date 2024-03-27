using System;
using System.Runtime.CompilerServices;

namespace Kotono.Utils
{
    public class Stopwatch
    {
        private double _startTime = Time.ExactSinceEpoch;

        /// <summary>
        /// Get the exact elapsed time since the Stopwatch started.
        /// </summary>
        public double ElapsedTime => Time.ExactSinceEpoch - _startTime;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start()
        {
            _startTime = Time.ExactSinceEpoch;
        }
    }
}
