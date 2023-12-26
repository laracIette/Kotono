using Assimp;
using Kotono.Graphics.Objects;
using OpenTK.Mathematics;
using System;
using Quaternion = OpenTK.Mathematics.Quaternion;

namespace Kotono.Utils
{
    public struct Vector
    {
        public float X;

        public float Y;

        public float Z;

        /// <summary> The length of the Vector </summary>
        public readonly float Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary> The Vector scaled to unit length </summary>
        public readonly Vector Normalized => this / Length;

        /// <summary> A Vector with X = 0, Y = 0, Z = 0 </summary>
        public static Vector Zero => new Vector(0, 0, 0);

        /// <summary> A Vector with X = 1, Y = 1, Z = 1 </summary>
        public static Vector Unit => new Vector(1, 1, 1);

        /// <summary> A Vector with X = 1, Y = 0, Z = 0 </summary>
        public static Vector UnitX => new Vector(1, 0, 0);

        /// <summary> A Vector with X = 1, Y = 1, Z = 0 </summary>
        public static Vector UnitXY => new Vector(1, 1, 0);

        /// <summary> A Vector with X = 1, Y = 0, Z = 1 </summary>
        public static Vector UnitXZ => new Vector(1, 0, 1);

        /// <summary> A Vector with X = 0, Y = 1, Z = 0 </summary>
        public static Vector UnitY => new Vector(0, 1, 0);

        /// <summary> A Vector with X = 0, Y = 1, Z = 1 </summary>
        public static Vector UnitYZ => new Vector(0, 1, 1);

        /// <summary> A Vector with X = 0, Y = 0, Z = 1 </summary>
        public static Vector UnitZ => new Vector(0, 0, 1);

        public static Vector Right => UnitX;

        public static Vector Up => UnitY;

        public static Vector Forward => UnitZ;

        public static int SizeInBytes => sizeof(float) * 3;

        public readonly float this[int index] =>
            index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => throw new IndexOutOfRangeException("You tried to access this Vector at index: " + index)
            };

        /// <summary> Initialize a Vector with X = 0, Y = 0, Z = 0 </summary>
        public Vector()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        /// <summary> Initialize a Vector with X = v.X, Y = v.Y, Z = v.Z </summary>
        public Vector(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        /// <summary> Initialize a Vector with X = f, Y = f, Z = f </summary>
        public Vector(float f)
        {
            X = f;
            Y = f;
            Z = f;
        }

        /// <summary> Initialize a Vector with X = (float)d, Y = (float)d, Z = (float)d </summary>
        public Vector(double d)
        {
            X = (float)d;
            Y = (float)d;
            Z = (float)d;
        }

        /// <summary> Initialize a Vector with X = x, Y = y, Z = z </summary>
        public Vector(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary> Initialize a Vector with X = (float)x, Y = (float)y, Z = (float)z </summary>
        public Vector(double x = 0, double y = 0, double z = 0)
        {
            X = (float)x;
            Y = (float)y;
            Z = (float)z;
        }

        public static Vector RotateAroundPoint(Vector v, Vector point, Vector rotation)
        {
            return (Vector)(Quaternion.FromEulerAngles((Vector3)rotation) * (Vector3)(v - point)) + point;
        }

        public static Vector Cross(Vector left, Vector right)
        {
            return new Vector
            {
                X = left.Y * right.Z - left.Z * right.Y,
                Y = left.Z * right.X - left.X * right.Z,
                Z = left.X * right.Y - left.Y * right.X
            };
        }

        public static float Dot(Vector left, Vector right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        /// <summary> Convert a Vector from degrees to radians </summary>
        public static Vector Rad(Vector v)
        {
            v.X = Math.Rad(v.X);
            v.Y = Math.Rad(v.Y);
            v.Z = Math.Rad(v.Z);
            return v;
        }

        /// <summary> Convert a Vector from radians to degrees </summary>
        public static Vector Deg(Vector v)
        {
            v.X = Math.Deg(v.X);
            v.Y = Math.Deg(v.Y);
            v.Z = Math.Deg(v.Z);
            return v;
        }

        public static float Distance(Vector left, Vector right)
        {
            return (left - right).Length;
        }

        public static float Distance(IObject3D left, IObject3D right)
        {
            return Distance(left.Location, right.Location);
        }

        public static Vector Abs(Vector v)
        {
            v.X = Math.Abs(v.X);
            v.Y = Math.Abs(v.Y);
            v.Z = Math.Abs(v.Z);
            return v;
        }

        public static float Min(Vector v)
        {
            return Math.Min(Math.Min(v.X, v.Y), v.Z);
        }

        public static float Max(Vector v)
        {
            return Math.Max(Math.Max(v.X, v.Y), v.Z);
        }

        public static Vector operator +(Vector left, Vector right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            return left;
        }

        public static Vector operator -(Vector left, Vector right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            return left;
        }

        public static Vector operator -(Vector v, float f)
        {
            v.X -= f;
            v.Y -= f;
            v.Z -= f;
            return v;
        }

        public static Vector operator -(Vector v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            v.Z = -v.Z;
            return v;
        }

        public static Vector operator *(Vector left, Vector right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            return left;
        }

        public static Vector operator *(Vector v, float f)
        {
            v.X *= f;
            v.Y *= f;
            v.Z *= f;
            return v;
        }

        public static Vector operator /(Vector left, Vector right)
        {
            left.X /= right.X;
            left.Y /= right.Y;
            left.Z /= right.Z;
            return left;
        }

        public static Vector operator /(Vector v, float f)
        {
            v.X /= f;
            v.Y /= f;
            v.Z /= f;
            return v;
        }

        public static bool operator ==(Vector left, Vector right)
        {
            return (left.X == right.X) && (left.Y == right.Y) && (left.Z == right.Z);
        }

        public static bool operator !=(Vector left, Vector right)
        {
            return !(left == right);
        }

        public static explicit operator Vector3(Vector v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector(Vector3 v)
        {
            return new Vector(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3D(Vector v)
        {
            return new Vector3D(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector(Vector3D v)
        {
            return new Vector(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector(float f)
        {
            return new Vector(f, f, f);
        }

        public override readonly string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}";
        }
    }
}
