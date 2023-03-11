namespace Kotono.Utils
{
    public static class Time
    {
        public static float Delta;

        public static void Update(float deltaTime)
        {
            Delta = deltaTime;
        }
    }
}
