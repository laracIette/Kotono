namespace Kotono.Utils
{
    public static class MathD
    {
        public const double PI = 3.14159265358979323846;

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        public static double Rad(double degrees)
        {
            return degrees * (PI / 180);
        }

        /// <summary>
        /// Converts radians to degrees
        /// </summary>
        public static double Deg(double radians)
        {
            return radians * (180 / PI);
        }

        public static double Abs(double value)
        {
            return (value >= 0) ? value : -value;
        }

        public static double Cos(double value)
        {
            return System.Math.Cos(value);
        }

        public static double Sin(double value)
        {
            return System.Math.Sin(value);
        }

        public static double Sqrt(double value)
        {
            return System.Math.Sqrt(value);
        }

        public static double Clamp(double value, double min, double max)
        {
            return System.Math.Clamp(value, min, max);
        }
    }
}
