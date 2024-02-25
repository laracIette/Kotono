using Kotono.Utils.Coordinates;
using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Kotono.Utils
{
    public static class Math
    {
        public const float PI = 3.14159265358979323846f;

        /// <summary> 
        /// Converts degrees to radians.
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Rad(float degrees)
        {
            return degrees * (PI / 180.0f);
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Deg(float radians)
        {
            return radians * (180.0f / PI);
        }

        /// <summary>
        /// Get the absolute value.
        /// </summary>
        [Pure]
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

        [Pure]
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

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float start, float end, float interpolation)
        {
            return start + (end - start) * interpolation;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float left, float right)
        {
            return (left < right) ? left : right;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float left, float right)
        {
            return (left > right) ? left : right;
        }

        /// <summary> 
        /// Loops a number in range [min, max).
        /// </summary>
        [Pure]
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
        /// Loops a number in range [0, max).
        /// </summary>
        [Pure]
        public static float Loop(float value, float max)
        {
            return Loop(value, 0.0f, max);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Floor(float value)
        {
            return (int)value;
        }
    }
}
