using System;

namespace Kotono.Utils
{
    public static class Time
    {
        private static double StartTime { get; } = ExactSinceEpoch;

        /// <summary>
        /// Current exact UTC Time since Epoch in seconds.
        /// </summary>
        public static double ExactSinceEpoch => DateTime.UtcNow.Ticks / (double)TimeSpan.TicksPerSecond;

        /// <summary>
        /// Current UTC Time since Epoch in seconds.
        /// </summary>
        public static double SinceEpoch { get; private set; } = ExactSinceEpoch;

        /// <summary>
        /// Current UTC Time since Epoch in milliseconds.
        /// </summary>
        public static double SinceEpochMS => SinceEpoch * 1000.0;

        /// <summary>
        /// Current Time since the start of the program in seconds.
        /// </summary>
        public static float Now { get; private set; } = 0.0f;

        /// <summary>
        /// Current Time since the start of the program in milliseconds.
        /// </summary>
        public static float NowMS => Now * 1000.0f;

        /// <summary>
        /// Delta Time in seconds.
        /// </summary>
        public static float Delta { get; private set; } = 0.0f;

        /// <summary>
        /// Delta Time in milliseconds.
        /// </summary>
        public static float DeltaMS => Delta * 1000.0f;

        public static void Update()
        {
            SinceEpoch = ExactSinceEpoch;

            float now = (float)(SinceEpoch - StartTime);

            Delta = now - Now;

            Now = now;
        }
    }
}
