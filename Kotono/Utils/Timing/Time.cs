using System;

namespace Kotono.Utils.Timing
{
    internal static class Time
    {
        /// <summary>
        /// DO NOT EDIT
        /// </summary>
        internal static double StartTime { get; set; }

        /// <summary>
        /// Current exact UTC Time since Epoch in seconds.
        /// </summary>
        internal static double ExactUTC => DateTime.UtcNow.Ticks / (double)TimeSpan.TicksPerSecond;

        /// <summary>
        /// Current UTC Time since Epoch in seconds.
        /// </summary>
        internal static double NowUTC { get; private set; } = ExactUTC;

        /// <summary>
        /// Current Time since the start of the program in seconds.
        /// </summary>
        internal static float Now { get; private set; } = 0.0f;

        /// <summary>
        /// Delta Time in seconds.
        /// </summary>
        internal static float Delta { get; private set; } = 0.0f;

        internal static void Update()
        {
            NowUTC = ExactUTC;

            float newNow = (float)(NowUTC - StartTime);

            Delta = newNow - Now;

            Now = newNow;
        }
    }
}
