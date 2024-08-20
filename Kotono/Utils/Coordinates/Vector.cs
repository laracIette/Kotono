using Assimp;
using Kotono.Graphics.Objects;
using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Quaternion = OpenTK.Mathematics.Quaternion;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector : IEquatable<Vector>
    {
        /// <summary> 
        /// The X component of the <see cref="Vector"/>. 
        /// </summary>
        [JsonInclude]
        public float X = 0.0f;

        /// <summary>
        /// The Y component of the <see cref="Vector"/>. 
        /// </summary>
        [JsonInclude]
        public float Y = 0.0f;

        /// <summary> 
        /// The Z component of the <see cref="Vector"/>. 
        /// </summary>
        [JsonInclude]
        public float Z = 0.0f;

        /// <summary>
        /// The component of the <see cref="Vector"/> corresponding to the index.
        /// </summary>
        /// <param name="index"> 0 => X, 1 => Y, 2 => Z. </param>
        /// <exception cref="SwitchException"></exception>
        public float this[int index]
        {
            readonly get => index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => throw new SwitchException(typeof(int), index)
            };
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    default:
                        throw new SwitchException(typeof(int), index);
                }
            }
        }

        /// <summary>
        /// The length of the <see cref="Vector"/>. 
        /// </summary>
        public readonly float Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary> 
        /// The <see cref="Vector"/> scaled to unit length.
        /// </summary>
        public readonly Vector Normalized => this / Length;

        /// <summary> 
        /// The minimum value of the <see cref="Vector"/>.
        /// </summary>
        public readonly float Min => Math.Min(Math.Min(X, Y), Z);

        /// <summary> 
        /// The maximum value of the <see cref="Vector"/>.
        /// </summary>
        public readonly float Max => Math.Max(Math.Max(X, Y), Z);

        /// <summary> 
        /// The absolute value of the <see cref="Vector"/>.
        /// </summary>
        public readonly Vector Abs => new(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));

        /// <summary> 
        /// Wether the <see cref="Vector"/> is equal to <see cref="Zero"/>.
        /// </summary>
        public readonly bool IsZero => this == Zero;

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

        public static Vector Up => UnitY;

        public static Vector Forward => UnitZ;

        public static Vector MinValue => new(float.MinValue, float.MinValue, float.MinValue);

        public static Vector MaxValue => new(float.MaxValue, float.MaxValue, float.MaxValue);

        public static int SizeInBytes => sizeof(float) * 3;

        /// <summary> 
        /// Initialize a <see cref="Vector"/> with X = x, Y = y, Z = z.
        /// </summary>
        public Vector(float x = 0.0f, float y = 0.0f, float z = 0.0f)
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
        /// Initialize a <see cref="Vector"/> with X = v.X, Y = v.Y, Z = v.Z.
        /// </summary>
        public Vector(Vector v) : this(v.X, v.Y, v.Z) { }

        /// <summary>
        /// Initialize a <see cref="Vector"/> with X = f, Y = f, Z = f.
        /// </summary>
        public Vector(float f) : this(f, f, f) { }

        public static Vector RotateAroundPoint(in Vector v, in Vector point, in Rotator rotation)
        {
            return (Vector)(Quaternion.FromEulerAngles((Vector3)rotation) * (Vector3)(v - point)) + point;
        }

        public static Vector Rotate(in Vector v, in Rotator r)
        {
            return (Vector)Vector3.Transform((Vector3)v, (Quaternion)r);
        }

        public static Vector Cross(in Vector left, in Vector right)
        {
            return new Vector
            {
                X = left.Y * right.Z - left.Z * right.Y,
                Y = left.Z * right.X - left.X * right.Z,
                Z = left.X * right.Y - left.Y * right.X
            };
        }

        public static float Dot(in Vector left, in Vector right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        /// <summary> 
        /// Convert a <see cref="Vector"/> from degrees to radians. 
        /// </summary>
        public static Vector Rad(Vector v)
        {
            v.X = Math.Rad(v.X);
            v.Y = Math.Rad(v.Y);
            v.Z = Math.Rad(v.Z);
            return v;
        }

        /// <summary> 
        /// Convert a <see cref="Vector"/> from radians to degrees. 
        /// </summary>
        public static Vector Deg(Vector v)
        {
            v.X = Math.Deg(v.X);
            v.Y = Math.Deg(v.Y);
            v.Z = Math.Deg(v.Z);
            return v;
        }

        public static float Distance(in Vector left, in Vector right)
        {
            return (left - right).Length;
        }

        internal static float Distance(in IObject3D left, in IObject3D right)
        {
            return Distance(left.RelativeLocation, right.RelativeLocation);
        }

        public static Vector Clamp(Vector v, float min, float max)
        {
            v.X = Math.Clamp(v.X, min, max);
            v.Y = Math.Clamp(v.Y, min, max);
            v.Z = Math.Clamp(v.Z, min, max);
            return v;
        }

        public static Vector Clamp(Vector v)
        {
            v.X = Math.Clamp(v.X);
            v.Y = Math.Clamp(v.Y);
            v.Z = Math.Clamp(v.Z);
            return v;
        }

        public static Vector Parse(in string[] values)
        {
            return new Vector
            {
                X = float.Parse(values[0]),
                Y = float.Parse(values[1]),
                Z = float.Parse(values[2])
            };
        }

        public static Vector Avg(params Vector[] values)
        {
            var result = Zero;
            foreach (var vector in values)
            {
                result += vector;
            }
            return result / values.Length;
        }

        public static Matrix4 CreateScaleMatrix(in Vector v)
        {
            return Matrix4.CreateScale((Vector3)v);
        }

        public static Matrix4 CreateTranslationMatrix(in Vector v)
        {
            return Matrix4.CreateTranslation((Vector3)v);
        }

        public static bool IsNullOrZero(Vector? v)
        {
            return v is null || v == Zero;
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

        public static Vector operator *(float f, Vector v)
        {
            v.X *= f;
            v.Y *= f;
            v.Z *= f;
            return v;
        }

        [Obsolete("Reorder operands, use \"Vector.operator *(float, Vector)\" instead.")]
        public static Vector operator *(Vector v, float f)
        {
            return f * v;
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
            return left.Equals(right);
        }

        public static bool operator !=(Vector left, Vector right)
        {
            return !(left == right);
        }

        public readonly void Deconstruct(out float x, out float y, out float z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Vector v && Equals(v);
        }

        public readonly bool Equals(Vector other)
        {
            return X == other.X
                && Y == other.Y
                && Z == other.Z;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
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
