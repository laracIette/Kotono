using System;

namespace Kotono.Utils.Timing
{
    internal class TimeoutEventArgs : EventArgs
    {
        /// <summary>
        /// The time at which the timeout occured.
        /// </summary>
        internal float Time { get; } = Timing.Time.Now;
    }
}
