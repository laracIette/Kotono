using System.Runtime.CompilerServices;

namespace Kotono.Utils
{
    public static class MathD
    {
        public const double PI = 3.14159265358979323846;

        /// <summary> 
        /// Converts degrees to radians.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Rad(double degrees)
        {
            return degrees * (PI / 180.0);
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Deg(double radians)
        {
            return radians * (180.0 / PI);
        }

        /// <summary>
        /// Get the absolute value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(double value)
        {
            return (value >= 0.0) ? value : -value;
        }

        /// <inheritdoc cref="System.Math.Cos(double)" />
        public static double Cos(double value)
        {
            return System.Math.Cos(value);
        }

        /// <inheritdoc cref="System.Math.Sin(double)" />
        public static double Sin(double value)
        {
            return System.Math.Sin(value);
        }

        /// <inheritdoc cref="System.Math.Sqrt(double)" />
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Lerp(double value, double min, double max)
        {
            return min + (max - min) * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(double left, double right)
        {
            return (left < right) ? left : right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(double left, double right)
        {
            return (left > right) ? left : right;
        }

        /// <summary> 
        /// Loops a number in range [min, max).
        /// </summary>
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

        /// <summary> 
        /// Loops a number in range [0, max).
        /// </summary>
        public static double Loop(double value, double max)
        {
            return Loop(value, 0.0, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Floor(double value)
        {
            return (int)value;
        }
    }
}
