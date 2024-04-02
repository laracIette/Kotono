using System;

namespace Kotono.Utils.Timing
{
    public static class Time
    {
        private static double StartTime { get; } = ExactUTC;

        /// <summary>
        /// Current exact UTC Time since Epoch in seconds.
        /// </summary>
        public static double ExactUTC => DateTime.UtcNow.Ticks / (double)TimeSpan.TicksPerSecond;

        /// <summary>
        /// Current UTC Time since Epoch in seconds.
        /// </summary>
        public static double NowUTC { get; private set; } = ExactUTC;

        /// <summary>
        /// Current Time since the start of the program in seconds.
        /// </summary>
        public static float Now { get; private set; } = 0.0f;

        /// <summary>
        /// Delta Time in seconds.
        /// </summary>
        public static float Delta { get; private set; } = 0.0f;

        public static void Update()
        {
            NowUTC = ExactUTC;

            float newNow = (float)(NowUTC - StartTime);

            Delta = newNow - Now;

            Now = newNow;
        }
    }
}
