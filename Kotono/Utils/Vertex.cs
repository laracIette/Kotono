namespace Kotono.Utils
{
    public struct Vertex
    {
        public Vector Location { get; set; }

        public Vector Normal { get; set; }

        public Point TexCoord { get; set; }

        public Vertex(Vector location, Vector normal, Point texCoord)
        {
            Location = location;
            Normal = normal;
            TexCoord = texCoord;
        }

        public const int SizeInBytes = Vector.SizeInBytes * 2 + Point.SizeInBytes;
    }
}
