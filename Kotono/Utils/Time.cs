using System;

namespace Kotono.Utils
{
    internal static class Time
    {
        private static double StartTime { get; } = DateTime.UtcNow.Ticks / (double)TimeSpan.TicksPerSecond;

        /// <summary>
        /// Current UTC Time since Epoch in seconds.
        /// </summary>
        internal static double SinceEpoch { get; private set; } = StartTime;

        /// <summary>
        /// Current UTC Time since Epoch in milliseconds.
        /// </summary>
        internal static double SinceEpochMS => SinceEpoch * 1000.0;

        /// <summary>
        /// Current Time since the start of the program in seconds.
        /// </summary>
        internal static float Now { get; private set; } = 0.0f;

        /// <summary>
        /// Current Time since the start of the program in milliseconds.
        /// </summary>
        internal static float NowMS => Now * 1000.0f;

        /// <summary>
        /// Delta Time in seconds.
        /// </summary>
        internal static float Delta { get; private set; } = 0.0f;

        /// <summary>
        /// Delta Time in milliseconds.
        /// </summary>
        internal static float DeltaMS => Delta * 1000.0f;

        internal static void Update()
        {
            SinceEpoch = DateTime.UtcNow.Ticks / (double)TimeSpan.TicksPerSecond;

            float now = (float)(SinceEpoch - StartTime);

            Delta = now - Now;

            Now = now;
        }
    }
}
