using OpenTK.Mathematics;

namespace Kotono.Utils
{
    public struct Transform
    {
        public Vector Location;

        public Vector Rotation;

        public Vector Scale;

        public readonly Vector Right => (Vector)(Quaternion.FromEulerAngles((Vector3)Vector.Deg(Rotation)) * Vector3.UnitX);
        
        public readonly Vector Up => (Vector)(Quaternion.FromEulerAngles((Vector3)Vector.Deg(Rotation)) * Vector3.UnitY);
        
        public readonly Vector Forward => (Vector)(Quaternion.FromEulerAngles((Vector3)Vector.Deg(Rotation)) * Vector3.UnitZ);


        public const int SizeInBytes = Vector.SizeInBytes * 3;

        public Transform() 
        { 
            Location = Vector.Zero;
            Rotation = Vector.Zero;
            Scale = Vector.Unit;
        }

        public Transform(Vector location, Vector rotation, Vector scale)
        {
            Location = location;
            Rotation = rotation;
            Scale = scale;
        }

        public readonly Matrix4 Model =>
            Matrix4.CreateScale((Vector3)Scale)
            * Matrix4.CreateRotationX(Rotation.X)
            * Matrix4.CreateRotationY(Rotation.Y)
            * Matrix4.CreateRotationZ(Rotation.Z)
            * Matrix4.CreateTranslation((Vector3)Location);
    }
}
