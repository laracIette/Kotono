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
        public static double NowS { get; private set; }

        /// <summary>
        /// Delta Time in milliseconds
        /// </summary>
        public static long Delta { get; private set; }

        /// <summary>
        /// Delta Time in seconds
        /// </summary>
        public static float DeltaS { get; private set; }

        public static void Update()
        {
            // current Time in milliseconds
            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond; 

            if (Now != 0)
            {
                Delta = now - Now;
                DeltaS = Delta / 1000f;
            }

            Now = now;
            NowS = Now / 1000.0;
        }
    }
}
