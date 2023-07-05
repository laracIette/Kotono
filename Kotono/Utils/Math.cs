namespace Kotono.Utils
{
    public static class Math
    {
        public const float PI = (float)MathD.PI;

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

        public static float Sqrt(double value)
        {
            return (float)MathD.Sqrt(value);
        }

        public static float Clamp(double value, double min, double max)
        {
            return (float)MathD.Clamp(value, min, max);
        }

        public static float Lerp(double start, double end, double interpolation)
        {
            return (float)MathD.Lerp(start, end, interpolation);
        }
    }
}
