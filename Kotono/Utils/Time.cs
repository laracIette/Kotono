using System;

namespace Kotono.Utils
{
    public static class Time
    {
        /// <summary>
        /// Current Time since Epoch in milliseconds.
        /// </summary>
        public static long SinceEpochMS { get; private set; }

        /// <summary>
        /// Current Time since Epoch in seconds.
        /// </summary>
        public static double SinceEpoch => SinceEpochMS / 1000.0;

        /// <summary>
        /// Current Time since the start of the program in milliseconds.
        /// </summary>
        public static int NowMS { get; private set; }

        /// <summary>
        /// Current Time since the start of the program in seconds.
        /// </summary>
        public static float Now => NowMS / 1000.0f;

        /// <summary>
        /// Delta Time in milliseconds.
        /// </summary>
        public static int DeltaMS { get; private set; }

        /// <summary>
        /// Delta Time in seconds.
        /// </summary>
        public static float Delta => DeltaMS / 1000.0f;

        static Time()
        {
            SinceEpochMS = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            NowMS = 0;
        }

        public static void Update()
        {
            long sinceEpoch = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            DeltaMS = (int)(sinceEpoch - SinceEpochMS);

            NowMS += DeltaMS;

            SinceEpochMS = sinceEpoch;
        }
    }
}
