using System;

namespace Kotono.Utils
{
    public static class Time
    {
        /// <summary>
        /// Current Time in milliseconds
        /// </summary>
        public static long Now { get; private set; }

        /// <summary>
        /// Current Time in seconds
        /// </summary>
        public static double NowS => Now / 1000.0;

        /// <summary>
        /// Delta Time in milliseconds
        /// </summary>
        public static long Delta { get; private set; }

        /// <summary>
        /// Delta Time in seconds
        /// </summary>
        public static float DeltaS => Delta / 1000f;

        static Time()
        {
            Now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public static void Update()
        {
            // current Time in milliseconds
            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            Delta = now - Now;

            Now = now;
        }
    }
}
