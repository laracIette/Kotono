using OpenTK.Mathematics;

namespace Kotono.Utils
{
    
    public static class Random
    {
        private static readonly System.Random _random = new System.Random();

        /// <summary>
        /// An int between min and max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>An int between min and max.</returns>
        public static int Int(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>A float between min and max.</returns>
        public static float Float(float min, float max)
        {
            return min + ((float)_random.NextDouble() * (max - min));
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>A random Vector3 with all values between min and max.</returns>
        public static Vector3 Vector3(float min, float max)
        {
            return new Vector3(
                Float(min, max),
                Float(min, max),
                Float(min, max)
            );
        }

        /// <summary>
        ///   
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        /// <param name="minZ"></param>
        /// <param name="maxZ"></param>
        /// <returns>A Vector3 with the X value between minX and maxX, the Y value between minY and maxY and the Z value between minZ and maxZ.</returns>
        public static Vector3 Vector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector3(
                Float(minX, maxX),
                Float(minY, maxY),
                Float(minZ, maxZ)
            );
        }
    }
}
