namespace Kotono.Utils
{
    public static class MathD
    {
        public const double PI = 3.14159265358979323846;

        /// <summary> Converts degrees to radians </summary>
        public static double Rad(double degrees)
        {
            return degrees * (PI / 180);
        }

        /// <summary> Converts radians to degrees </summary>
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
            if (min > max)
            {
                (min, max) = (max, min);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        public static double Lerp(double start, double end, double interpolation)
        {
            return start + (end - start) * interpolation;
        }

        public static double Min(double left, double right)
        {
            return (left < right) ? left : right;
        }

        public static double Max(double left, double right)
        {
            return (left > right) ? left : right;
        }

        /// <summary> Loops a number in range [min, max) </summary>
        public static double Loop(double value, double min, double max)
        {
            if (min > max)
            {
                (min, max) = (max, min);
            }

            if (value >= max)
            {
                value = (value - min) % (max - min) + min;
            }
            else if (value < min)
            {
                value = (value - min) % (max - min) + max;
            }

            return value;
        }

        /// <summary> Loops a number in range [0, max) </summary>
        public static double Loop(double value, double max)
        {
            return Loop(value, 0, max);
        }
    }
}
