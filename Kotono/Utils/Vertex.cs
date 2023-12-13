namespace Kotono.Utils
{
    public struct Vertex(Vector location, Vector normal, Point texCoord)
    {
        public Vector Location = location;

        public Vector Normal = normal;

        public Point TexCoord = texCoord;

        public const int SizeInBytes = Vector.SizeInBytes * 2 + Point.SizeInBytes;
    }
}
