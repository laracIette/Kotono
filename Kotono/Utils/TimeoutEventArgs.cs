using System;

namespace Kotono.Utils
{
    internal class TimeoutEventArgs : EventArgs
    {
        /// <summary>
        /// The time at which the timeout occured.
        /// </summary>
        internal float Time { get; } = Utils.Time.Now;
    }
}
