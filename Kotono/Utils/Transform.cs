using OpenTK.Mathematics;

namespace Kotono.Utils
{
    public struct Transform
    {
        private Vector _location;

        private Vector _rotation;

        private Vector _scale;

        public Vector Location
        {
            readonly get => _location;
            set
            {
                _location = value;
            }
        }

        public Vector Rotation
        {
            readonly get => _rotation;
            set
            {
                _rotation = value;
                UpdateVectors();
            }
        }

        public Vector Scale
        {
            readonly get => _scale;
            set
            {
                _scale = value;
            }
        }

        public Vector Right { get; private set; }


        public Vector Up { get; private set; }


        public Vector Forward { get; private set; }


        public const int SizeInBytes = Vector.SizeInBytes * 3;

        public Transform() 
        { 
            Location = Vector.Zero;
            Rotation = Vector.Zero;
            Scale = Vector.Unit;
        }

        public Transform(Transform t) 
        { 
            Location = t.Location;
            Rotation = t.Rotation;
            Scale = t.Scale;
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
        
        private void UpdateVectors()
        {
            Right = (Vector)(Quaternion.FromEulerAngles((Vector3)Rotation) * Vector3.UnitX);
            Up = (Vector)(Quaternion.FromEulerAngles((Vector3)Rotation) * Vector3.UnitY);
            Forward = (Vector)(Quaternion.FromEulerAngles((Vector3)Rotation) * Vector3.UnitZ);
        }

        public override readonly string ToString()
        {
            return $"Location: {Location}\nRotation: {Rotation}\nScale   : {Scale}";
        }
    }
}
