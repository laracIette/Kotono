namespace Kotono.Utils
{

    public static class Random
    {
        private static readonly System.Random _random = new();

        /// <summary>
        /// Get an int between min and max.
        /// </summary>
        public static int Int(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Get a float between min and max.
        /// </summary>
        public static float Float(float min, float max)
        {
            return Math.Lerp(min, max, _random.NextSingle());
        }

        /// <summary>
        /// Get a double between min and max.
        /// </summary>
        public static double Double(double min, double max)
        {
            return MathD.Lerp(min, max, _random.NextDouble());
        }

        /// <summary>
        /// Get a Vector with all values between min and max.
        /// </summary>
        public static Vector Vector(float min, float max)
        {
            return new Vector(
                Float(min, max),
                Float(min, max),
                Float(min, max)
            );
        }

        /// <summary>
        /// Get a Vector with all values between 0 and 1.
        /// </summary>
        public static Vector Vector()
        {
            return Vector(0.0f, 1.0f);
        }

        /// <summary>
        /// Get a Vector with the X value between minX and maxX, the Y value between minY and maxY and the Z value between minZ and maxZ.
        /// </summary>
        public static Vector Vector(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector(
                Float(minX, maxX),
                Float(minY, maxY),
                Float(minZ, maxZ)
            );
        }

        /// <summary>
        /// Get a Color with all values apart from A between min and max.
        /// </summary>
        public static Color Color(float min, float max)
        {
            return new Color(
                Float(min, max),
                Float(min, max),
                Float(min, max)
            );
        }

        /// <summary>
        /// Get a Color with all values apart from A between 0 and 1.
        /// </summary>
        public static Color Color()
        {
            return Color(0.0f, 1.0f);
        }
    }
}
