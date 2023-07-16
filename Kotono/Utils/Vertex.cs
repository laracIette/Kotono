namespace Kotono.Utils
{
    public struct Vertex
    {
        public Vector Location;

        public Vector Normal;

        public Point TexCoord;

        public Vertex(Vector location, Vector normal, Point texCoord)
        {
            Location = location;
            Normal = normal;
            TexCoord = texCoord;
        }

        public const int SizeInBytes = Vector.SizeInBytes * 2 + Point.SizeInBytes;
    }
}
