using Kotono.File;
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

        public static Transform FromProperties(Properties properties)
        {
            return new Transform
            {
                Location = new Vector
                {
                    X = float.Parse(properties["Transform.Location.X"]),
                    Y = float.Parse(properties["Transform.Location.Y"]),
                    Z = float.Parse(properties["Transform.Location.Z"])
                },
                Rotation = new Vector
                {
                    X = float.Parse(properties["Transform.Rotation.X"]),
                    Y = float.Parse(properties["Transform.Rotation.Y"]),
                    Z = float.Parse(properties["Transform.Rotation.Z"])
                },
                Scale = new Vector
                {
                    X = float.Parse(properties["Transform.Scale.X"]),
                    Y = float.Parse(properties["Transform.Scale.Y"]),
                    Z = float.Parse(properties["Transform.Scale.Z"])
                }
            };
        }

        public static bool operator ==(Transform left, Transform right)
        {
            return (left.Location == right.Location) && (left.Rotation == right.Rotation) && (left.Scale == right.Scale);
        }

        public static bool operator !=(Transform left, Transform right)
        {
            return !(left == right);
        }

        public override readonly string ToString()
        {
            return $"Location: {Location}\nRotation: {Rotation}\nScale   : {Scale}";
        }
    }
}
