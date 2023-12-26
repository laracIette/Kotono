using System;
using System.Runtime.InteropServices;

namespace Kotono.Utils
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex(Vector location, Vector normal, Point texCoord)
    {
        /// <summary> 
        /// The location component of the Vertex. 
        /// </summary>
        public Vector Location = location;

        /// <summary> 
        /// The normal component of the Vertex.
        /// </summary>
        public Vector Normal = normal;

        /// <summary>
        /// The texture coordinates component of the Vertex.
        /// </summary>
        public Point TexCoord = texCoord;

        public static int SizeInBytes => Vector.SizeInBytes * 2 + Point.SizeInBytes;
    }
}
