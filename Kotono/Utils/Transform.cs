namespace Kotono.Utils
{
    public struct Transform
    {
        public Vector Location;

        public Vector Rotation;

        public Vector Scale;


        public const int SizeInBytes = Vector.SizeInBytes * 3;

        public Transform() 
        { 
            Location = Vector.Zero;
            Rotation = Vector.Zero;
            Scale = Vector.One;
        }

        public Transform(Vector location, Vector rotation, Vector scale)
        {
            Location = location;
            Rotation = rotation;
            Scale = scale;
        }

    }
}
