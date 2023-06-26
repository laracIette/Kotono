namespace Kotono.Utils
{
    public static class Math
    {
        public const float PI = 3.1415927f;

        /// <summary> Converts degrees to radians </summary>
        public static float Rad(double degrees)
        {
            return (float)MathD.Rad(degrees);
        }

        /// <summary> Converts radians to degrees </summary>
        public static float Deg(double radians)
        {
            return (float)MathD.Deg(radians);
        }

        public static float Abs(double value)
        {
            return (float)MathD.Abs(value);
        }

        public static float Cos(double value)
        {
            return (float)MathD.Cos(value);
        }

        public static float Sin(double value)
        {
            return (float)MathD.Sin(value);
        }
    }
}
