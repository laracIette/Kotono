namespace Kotono.Utils
{
    internal class TimedEventArgs : System.EventArgs
    {
        /// <summary>
        /// The time at which the event occured.
        /// </summary>
        internal float Time { get; } = Timing.Time.Now;
    }
}
