using OpenTK.Mathematics;
using System;

namespace Kotono.Utils
{
    public struct Vector
    {
        public float X;

        public float Y;

        public float Z;

        public readonly float Length => MathF.Sqrt(X * X + Y * Y + Z * Z);

        public readonly Vector Normalized => this / Length;

        public static Vector UnitX => new Vector(1, 0, 0);

        public static Vector UnitXY => new Vector(1, 1, 0);

        public static Vector UnitXZ => new Vector(1, 0, 1);

        public static Vector UnitY => new Vector(0, 1, 0);

        public static Vector UnitYZ => new Vector(0, 1, 1);

        public static Vector UnitZ => new Vector(0, 0, 1);

        public static Vector One => new Vector(1, 1, 1);

        public static Vector Zero => new Vector(0, 0, 0);

        public const int SizeInBytes = sizeof(float) * 3;

        public readonly float this[int index] =>
            index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => throw new IndexOutOfRangeException("You tried to access this Vector at index: " + index)
            };
            
        
        public Vector()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Vector(float f)
        {
            X = f;
            Y = f;
            Z = f;
        }

        public Vector(double d)
        {
            X = (float)d;
            Y = (float)d;
            Z = (float)d;
        }

        public Vector(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void RotateAroundAxis(Vector axis, float angle)
        {
            // Create a Quaternion representing the rotation
            var rotation = Quaternion.FromAxisAngle((Vector3)axis, MathHelper.DegreesToRadians(angle));

            // Rotate the vector using the Quaternion
            this = (Vector)Vector3.Transform((Vector3)this, rotation);
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

        public static Vector operator -(Vector v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            v.Z = -v.Z;
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

        public static bool operator ==(Vector left, Vector right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
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

        public override readonly string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}"; ;
        }

    }
}
