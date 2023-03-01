using OpenTK.Mathematics;

namespace Kotono.Utils
{
    
    public static class Random
    {
        private static readonly System.Random _random = new System.Random();

        /// <summary>
        /// Returns an int between min and max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetInt(int min, int max)
        {
            return _random.Next(min, max);
        }
        
        /// <summary>
         /// Returns a float between min and max.
         /// </summary>
         /// <param name="min"></param>
         /// <param name="max"></param>
         /// <returns></returns>
        public static float GetFloat(float min, float max)
        {
            return min + ((float)_random.NextDouble() * (max - min));
        }

        /// <summary>
        /// Returns a random Vector3 with all values between min and max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3 GetVector3(float min, float max)
        {
            return new Vector3(
                GetFloat(min, max),
                GetFloat(min, max),
                GetFloat(min, max)
            );
        }

        /// <summary>
        /// Returns a Vector3 with the X value between minX and maxX, the Y value between minY and maxY and the Z value between minZ and maxZ. 
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        /// <param name="minZ"></param>
        /// <param name="maxZ"></param>
        /// <returns></returns>
        public static Vector3 GetVector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector3(
                GetFloat(minX, maxX),
                GetFloat(minY, maxY),
                GetFloat(minZ, maxZ)
            );
        }
    }
}
