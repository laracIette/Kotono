using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Kotono.Utils
{
    public static class Math
    {
        public const float PI = 3.14159265358979323846f;

        /// <summary> 
        /// Convert degrees to radians.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Rad(float degrees)
        {
            return degrees * (PI / 180.0f);
        }

        /// <summary>
        /// Convert radians to degrees.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Deg(float radians)
        {
            return radians * (180.0f / PI);
        }

        /// <summary>
        /// Get the absolute value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float value)
        {
            return (value >= 0.0f) ? value : -value;
        }

        /// <inheritdoc cref="System.MathF.Cos(float)" />
        public static float Cos(float value)
        {
            return System.MathF.Cos(value);
        }

        /// <inheritdoc cref="System.MathF.Sin(float)" />
        public static float Sin(float value)
        {
            return System.MathF.Sin(value);
        }

        /// <inheritdoc cref="System.MathF.Sqrt(float)" />
        public static float Sqrt(float value)
        {
            return System.MathF.Sqrt(value);
        }

        /// <summary>
        /// Clamp a value in range [min, max].
        /// </summary>
        public static float Clamp(float value, float min, float max)
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

        /// <summary>
        /// Clamp a value in range [0, 1].
        /// </summary>
        public static float Clamp(float value)
        {
            return Clamp(value, 0.0f, 1.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float value, float min, float max)
        {
            return min + (max - min) * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float left, float right)
        {
            return (left < right) ? left : right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float left, float right)
        {
            return (left > right) ? left : right;
        }

        /// <summary> 
        /// Loop a number in range [min, max).
        /// </summary>
        public static float Loop(float value, float min, float max)
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
        /// Loop a number in range [0, max).
        /// </summary>
        public static float Loop(float value, float max)
        {
            return Loop(value, 0.0f, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Floor(float value)
        {
            return (int)value;
        }

        public static float Add(params float[] values)
        {
            float result = 0.0f;

            foreach (var item in values)
            {
                result += item;
            }

            return result;
        }

        public static float Avg(params float[] values)
        {
            return Add(values) / values.Length;
        }
    }
}
