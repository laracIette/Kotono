using Kotono.Utils.Coordinates;

namespace Kotono.Utils
{

    public static class Random
    {
        private static System.Random Shared => System.Random.Shared;

        /// <summary>
        /// Get an int in range [min, max).
        /// </summary>
        public static int Int(int min, int max, int? seed = null)
        {
            return seed.HasValue ? new System.Random(seed.Value).Next(min, max) : Shared.Next(min, max);
        }

        /// <summary>
        /// Get a long in range [min, max).
        /// </summary>
        public static long Long(long min, long max, int? seed = null)
        {
            return seed.HasValue ? new System.Random(seed.Value).NextInt64(min, max) : Shared.NextInt64(min, max);
        }

        /// <summary>
        /// Get a float in range [min, max).
        /// </summary>
        public static float Float(float min, float max, int? seed = null)
        {
            return Math.Lerp(min, max, seed.HasValue ? new System.Random(seed.Value).NextSingle() : Shared.NextSingle());
        }

        /// <summary>
        /// Get a double in range [min, max).
        /// </summary>
        public static double Double(double min, double max, int? seed = null)
        {
            return MathD.Lerp(min, max, seed.HasValue ? new System.Random(seed.Value).NextDouble() : Shared.NextDouble());
        }

        /// <summary>
        /// Get a Vector with all values in range [min, max).
        /// </summary>
        public static Vector Vector(float min, float max, int? seed = null)
        {
            return new Vector(
                Float(min, max, seed),
                Float(min, max, seed),
                Float(min, max, seed)
            );
        }

        /// <summary>
        /// Get a Vector with all values in range [0, 1).
        /// </summary>
        public static Vector Vector(int? seed = null)
        {
            return Vector(0.0f, 1.0f, seed);
        }

        /// <summary>
        /// Get a Vector with the X value in range [minX, maxX), the Y value in range [minY, maxY) and the Z value in range [minZ, maxZ).
        /// </summary>
        public static Vector Vector(float minX, float maxX, float minY, float maxY, float minZ, float maxZ, int? seed = null)
        {
            return new Vector(
                Float(minX, maxX, seed),
                Float(minY, maxY, seed),
                Float(minZ, maxZ, seed)
            );
        }

        /// <summary>
        /// Get a Color with all values apart from A in range [min, max).
        /// </summary>
        public static Color Color(float min, float max, int? seed = null)
        {
            return new Color(
                Float(min, max, seed),
                Float(min, max, seed),
                Float(min, max, seed)
            );
        }

        /// <summary>
        /// Get a Color with all values apart from A in range [0, 1).
        /// </summary>
        public static Color Color(int? seed = null)
        {
            return Color(0.0f, 1.0f, seed);
        }
    }
}
