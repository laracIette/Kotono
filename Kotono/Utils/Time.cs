using System;

namespace Kotono.Utils
{
    public static class Time
    {
        public static float Delta { get; private set; }

        public static long Now { get; private set; }

        public static int Milliseconds { get; private set; }

        public static void Update()
        {
            var now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond; 
            if (Now != 0)
            {
                Delta = (now - Now) / 1000.0f;
            }
            Now = now;
        }
    }
}
