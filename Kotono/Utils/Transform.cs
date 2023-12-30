using Kotono.File;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Utils
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Transform
    {
        /// <summary> 
        /// The location component of the Transform. 
        /// </summary>
        public Vector Location;

        /// <summary> 
        /// The rotation component of the Transform. 
        /// </summary>
        public Vector Rotation;

        /// <summary> 
        /// The scale component of the Transform. 
        /// </summary>
        public Vector Scale;

        /// <summary> 
        /// The right vector of the Transform. 
        /// </summary>
        public readonly Vector Right => (Vector)(Quaternion.FromEulerAngles((Vector3)Rotation) * Vector3.UnitX);

        /// <summary> 
        /// The up vector of the Transform. 
        /// </summary>
        public readonly Vector Up => (Vector)(Quaternion.FromEulerAngles((Vector3)Rotation) * Vector3.UnitY);

        /// <summary> 
        /// The forward vector of the Transform. 
        /// </summary>
        public readonly Vector Forward => (Vector)(Quaternion.FromEulerAngles((Vector3)Rotation) * Vector3.UnitZ);

        /// <summary> 
        /// A Transform with Location = Vector.Zero, Rotation = Vector.Zero, Scale = Vector.Unit. 
        /// </summary>
        public static readonly Transform Default = new Transform(Vector.Zero, Vector.Zero, Vector.Unit);

        public static int SizeInBytes => Vector.SizeInBytes * 3;

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
            return left.Equals(right);
        }

        public static bool operator !=(Transform left, Transform right)
        {
            return !(left == right);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Transform t && Equals(t);
        }

        public readonly bool Equals(Transform t)
        {
            return Location == t.Location
                && Rotation == t.Rotation
                && Scale == t.Scale;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Location, Rotation, Scale);
        }

        public override readonly string ToString()
        {
            return $"Location: {Location}\nRotation: {Rotation}\nScale   : {Scale}";
        }
    }
}
