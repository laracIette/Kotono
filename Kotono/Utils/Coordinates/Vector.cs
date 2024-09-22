using Assimp;
using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;
using static Catalyst.Models.English;
using Quaternion = OpenTK.Mathematics.Quaternion;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Vector : IEquatable<Vector>
    {
        /// <summary> 
        /// The X component of the <see cref="Vector"/>. 
        /// </summary>
        public readonly float X;

        /// <summary>
        /// The Y component of the <see cref="Vector"/>. 
        /// </summary>
        public readonly float Y;

        /// <summary> 
        /// The Z component of the <see cref="Vector"/>. 
        /// </summary>
        public readonly float Z;

        /// <summary>
        /// The length of the <see cref="Vector"/>. 
        /// </summary>
        public readonly float Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary> 
        /// The <see cref="Vector"/> scaled to unit length.
        /// </summary>
        public readonly Vector Normalized => this / Length;

        /// <summary>
        /// A <see cref="Vector"/> with X = 0, Y = 0, Z = 0.
        /// </summary>
        public static Vector Zero => new(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// A <see cref="Vector"/> with X = 1, Y = 1, Z = 1. 
        /// </summary>
        public static Vector Unit => new(1.0f, 1.0f, 1.0f);

        /// <summary> 
        /// A <see cref="Vector"/> with X = 1, Y = 0, Z = 0.
        /// </summary>
        public static Vector UnitX => new(1.0f, 0.0f, 0.0f);

        /// <summary> 
        /// A <see cref="Vector"/> with X = 1, Y = 1, Z = 0. 
        /// </summary>
        public static Vector UnitXY => new(1.0f, 1.0f, 0.0f);

        /// <summary> 
        /// A <see cref="Vector"/> with X = 1, Y = 0, Z = 1.
        /// </summary>
        public static Vector UnitXZ => new(1.0f, 0.0f, 1.0f);

        /// <summary> 
        /// A <see cref="Vector"/> with X = 0, Y = 1, Z = 0. 
        /// </summary>
        public static Vector UnitY => new(0.0f, 1.0f, 0.0f);

        /// <summary>
        /// A <see cref="Vector"/> with X = 0, Y = 1, Z = 1.
        /// </summary>
        public static Vector UnitYZ => new(0.0f, 1.0f, 1.0f);

        /// <summary>
        /// A <see cref="Vector"/> with X = 0, Y = 0, Z = 1.
        /// </summary>
        public static Vector UnitZ => new(0.0f, 0.0f, 1.0f);

        public static Vector Right => UnitX;

        public static Vector Left => -UnitX;

        public static Vector Up => UnitY;

        public static Vector Down => -UnitY;

        public static Vector Forward => UnitZ;

        public static Vector Backward => -UnitZ;

        public static Vector MinValue => new(float.MinValue, float.MinValue, float.MinValue);

        public static Vector MaxValue => new(float.MaxValue, float.MaxValue, float.MaxValue);

        public static int SizeInBytes => sizeof(float) * 3;

        /// <summary> 
        /// Initialize a <see cref="Vector"/> with X = x, Y = y, Z = z.
        /// </summary>
        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary> 
        /// Initialize a <see cref="Vector"/> with X = 0, Y = 0, Z = 0.
        /// </summary>
        public Vector() : this(0.0f, 0.0f, 0.0f) { }

        /// <summary>
        /// Initialize a <see cref="Vector"/> with X = f, Y = f, Z = f.
        /// </summary>
        public Vector(float f) : this(f, f, f) { }

        public static Vector RotateAroundPoint(Vector v, Vector point, in Rotator rotation)
            => (Vector)(Quaternion.FromEulerAngles((Vector3)rotation) * (Vector3)(v - point)) + point;

        public static Vector Rotate(Vector v, in Rotator r)
            => (Vector)Vector3.Transform((Vector3)v, (Quaternion)r);

        public static Vector Cross(Vector left, Vector right)
            => new(
                left.Y * right.Z - left.Z * right.Y,
                left.Z * right.X - left.X * right.Z,
                left.X * right.Y - left.Y * right.X
            );

        public static float Dot(Vector left, Vector right)
            => left.X * right.X + left.Y * right.Y + left.Z * right.Z;

        /// <summary> 
        /// Get the minimum value of the <see cref="Vector"/>.
        /// </summary>
        public static float Min(Vector v)
            => Math.Min(Math.Min(v.X, v.Y), v.Z);

        /// <summary> 
        /// Get the maximum value of the <see cref="Vector"/>.
        /// </summary>
        public static float Max(Vector v)
            => Math.Max(Math.Max(v.X, v.Y), v.Z);

        /// <summary> 
        /// Get the absolute values of the <see cref="Vector"/>. 
        /// </summary>
        public static Vector Abs(Vector v)
            => new(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z));

        /// <summary> 
        /// Convert a <see cref="Vector"/> from degrees to radians. 
        /// </summary>
        public static Vector Rad(Vector v)
            => new(Math.Rad(v.X), Math.Rad(v.Y), Math.Rad(v.Z));

        /// <summary> 
        /// Convert a <see cref="Vector"/> from radians to degrees. 
        /// </summary>
        public static Vector Deg(Vector v)
            => new(Math.Deg(v.X), Math.Deg(v.Y), Math.Deg(v.Z));

        public static float Distance(Vector left, Vector right)
            => (left - right).Length;

        internal static float Distance(in ITransform left, in ITransform right)
            => Distance(left.WorldLocation, right.WorldLocation);

        public static Vector Clamp(Vector v, float min, float max)
            => new(Math.Clamp(v.X, min, max), Math.Clamp(v.Y, min, max), Math.Clamp(v.Z, min, max));

        public static Vector Clamp(Vector v)
            => new(Math.Clamp(v.X), Math.Clamp(v.Y), Math.Clamp(v.Z));

        public static Vector ClampLength(Vector v, float minLength, float maxLength)
        {
            ExceptionHelper.ThrowIf(minLength < 0.0f, "minLength must not be negative");
            ExceptionHelper.ThrowIf(minLength > maxLength, "minLength must not be over maxLength");

            if (v.Length < minLength)
            {
                return minLength * v.Normalized;
            }

            if (v.Length > maxLength)
            {
                return maxLength * v.Normalized;
            }

            return v;
        }

        /// <summary>
        /// Get the <see cref="Vector"/> with the smallest length.
        /// </summary>
        public static Vector MinLength(Vector v, float minLength)
        {
            ExceptionHelper.ThrowIf(minLength < 0.0f, "minLength must not be negative");
           
            if (v.Length > minLength)
            {
                return minLength * v.Normalized;
            }

            return v;
        }

        /// <summary>
        /// Get the <see cref="Vector"/> with the biggest length.
        /// </summary>
        public static Vector MaxLength(Vector v, float maxLength)
        {
            ExceptionHelper.ThrowIf(maxLength < 0.0f, "maxLength must not be negative");
           
            if (v.Length < maxLength)
            {
                return maxLength * v.Normalized;
            }

            return v;
        }

        public static Vector Parse(in string[] values)
            => new(float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[0]));

        public static Vector Avg(params Vector[] values)
        {
            var result = Zero;
            foreach (var vector in values)
            {
                result += vector;
            }
            return result / values.Length;
        }

        public static Matrix4 CreateScaleMatrix(Vector v)
            => Matrix4.CreateScale((Vector3)v);

        public static Matrix4 CreateTranslationMatrix(Vector v)
            => Matrix4.CreateTranslation((Vector3)v);

        public static bool IsNullOrZero(Vector? v)
            => v is null || v == Zero;

        public static Vector operator +(Vector left, Vector right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector operator +(float f, Vector v)
            => new(v.X + f, v.Y + f, v.Z + f);

        [Obsolete("Reorder operands, use 'Vector.operator +(float, Vector)' instead.")]
        public static Vector operator +(Vector v, float f)
            => f + v;

        public static Vector operator -(Vector left, Vector right)
            => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Vector operator -(Vector v, float f)
            => new(v.X - f, v.Y - f, v.Z - f);

        public static Vector operator -(Vector v)
            => new(-v.X, -v.Y, -v.Z);

        public static Vector operator *(Vector left, Vector right)
            => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        public static Vector operator *(float f, Vector v)
            => new(v.X * f, v.Y * f, v.Z * f);

        [Obsolete("Reorder operands, use 'Vector.operator *(float, Vector)' instead.")]
        public static Vector operator *(Vector v, float f)
            => f * v;

        public static Vector operator /(Vector left, Vector right)
            => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        public static Vector operator /(Vector v, float f)
            => new(v.X / f, v.Y / f, v.Z / f);

        public static bool operator ==(Vector left, Vector right)
            => left.Equals(right);

        public static bool operator !=(Vector left, Vector right)
            => !(left == right);

        public readonly void Deconstruct(out float x, out float y, out float z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public override readonly bool Equals(object? obj)
            => obj is Vector v && Equals(v);

        public readonly bool Equals(Vector other)
            => X == other.X
            && Y == other.Y
            && Z == other.Z;

        public override readonly int GetHashCode()
            => HashCode.Combine(X, Y, Z);

        public static explicit operator Vector3(Vector v)
            => new(v.X, v.Y, v.Z);

        public static explicit operator Vector(Vector3 v)
            => new(v.X, v.Y, v.Z);

        public static explicit operator Vector3D(Vector v)
            => new(v.X, v.Y, v.Z);

        public static explicit operator Vector(Vector3D v)
            => new(v.X, v.Y, v.Z);

        public static explicit operator Vector(float f)
            => new(f, f, f);

        public override readonly string ToString()
            => $"X: {X}, Y: {Y}, Z: {Z}";
    }
}
