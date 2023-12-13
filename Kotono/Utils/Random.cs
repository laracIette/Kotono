namespace Kotono.Utils
{

    public static class Random
    {
        private static readonly System.Random _random = new();

        /// <returns>An int between min and max.</returns>
        public static int Int(int min, int max)
        {
            return _random.Next(min, max);
        }


        /// <returns>A double between min and max.</returns>
        public static double Double(double min, double max)
        {
            return MathD.Lerp(min, max, _random.NextDouble());
        }


        /// <returns>A float between min and max.</returns>
        public static float Float(float min, float max)
        {
            return (float)Double(min, max);
        }

        /// <returns>A Vector with all values between min and max.</returns>
        public static Vector Vector(float min, float max)
        {
            return new Vector(
                Float(min, max),
                Float(min, max),
                Float(min, max)
            );
        }

        /// <returns>A Vector with the X value between minX and maxX, the Y value between minY and maxY and the Z value between minZ and maxZ.</returns>
        public static Vector Vector(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector(
                Float(minX, maxX),
                Float(minY, maxY),
                Float(minZ, maxZ)
            );
        }

        /// <returns>A Color with all values apart from A between min and max.</returns>
        public static Color Color(float min, float max)
        {
            return new Color(
                Float(min, max),
                Float(min, max),
                Float(min, max)
            );
        }

        /// <returns>A Color with all values apart from A between 0 and 1.</returns>
        public static Color Color()
        {
            return Color(0, 1);
        }
    }
}
