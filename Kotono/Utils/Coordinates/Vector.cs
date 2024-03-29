﻿using Assimp;
using Kotono.Graphics.Objects;
using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Quaternion = OpenTK.Mathematics.Quaternion;

namespace Kotono.Utils.Coordinates
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector : IEquatable<Vector>
    {
        /// <summary> 
        /// The X component of the Vector. 
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The Y component of the Vector. 
        /// </summary>
        public float Y { get; set; }

        /// <summary> 
        /// The Z component of the Vector. 
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// The component of the Vector corresponding to the index.
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
        /// The length of the Vector. 
        /// </summary>
        [JsonIgnore]
        public readonly float Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary> 
        /// The Vector scaled to unit length.
        /// </summary>
        [JsonIgnore]
        public readonly Vector Normalized => this / Length;

        /// <summary> 
        /// The minimum value of the Vector.
        /// </summary>
        [JsonIgnore]
        public readonly float Min => Math.Min(Math.Min(X, Y), Z);

        /// <summary> 
        /// The maximum value of the Vector.
        /// </summary>
        [JsonIgnore]
        public readonly float Max => Math.Max(Math.Max(X, Y), Z);

        /// <summary> 
        /// The absolute value of the Vector.
        /// </summary>
        [JsonIgnore]
        public readonly Vector Abs => new Vector(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));

        /// <summary>
        /// A <see cref="Vector"/> with X = 0, Y = 0, Z = 0.
        /// </summary>
        public static Vector Zero => new Vector(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// A <see cref="Vector"/> with X = 1, Y = 1, Z = 1. 
        /// </summary>
        public static Vector Unit => new Vector(1.0f, 1.0f, 1.0f);

        /// <summary> 
        /// A <see cref="Vector"/> with X = 1, Y = 0, Z = 0.
        /// </summary>
        public static Vector UnitX => new Vector(1.0f, 0.0f, 0.0f);

        /// <summary> 
        /// A <see cref="Vector"/> with X = 1, Y = 1, Z = 0. 
        /// </summary>
        public static Vector UnitXY => new Vector(1.0f, 1.0f, 0.0f);

        /// <summary> 
        /// A <see cref="Vector"/> with X = 1, Y = 0, Z = 1.
        /// </summary>
        public static Vector UnitXZ => new Vector(1.0f, 0.0f, 1.0f);

        /// <summary> 
        /// A <see cref="Vector"/> with X = 0, Y = 1, Z = 0. 
        /// </summary>
        public static Vector UnitY => new Vector(0.0f, 1.0f, 0.0f);

        /// <summary>
        /// A <see cref="Vector"/> with X = 0, Y = 1, Z = 1.
        /// </summary>
        public static Vector UnitYZ => new Vector(0.0f, 1.0f, 1.0f);

        /// <summary>
        /// A <see cref="Vector"/> with X = 0, Y = 0, Z = 1.
        /// </summary>
        public static Vector UnitZ => new Vector(0.0f, 0.0f, 1.0f);

        public static Vector Right => UnitX;

        public static Vector Up => UnitY;

        public static Vector Forward => UnitZ;

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

        public static Vector RotateAroundPoint(Vector v, Vector point, Rotator rotation)
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

        public static float Distance(Vector left, Vector right)
        {
            return (left - right).Length;
        }

        internal static float Distance(IObject3D left, IObject3D right)
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

        public static Vector Parse(string[] values)
        {
            return new Vector
            {
                X = float.Parse(values[0]),
                Y = float.Parse(values[1]),
                Z = float.Parse(values[2])
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

        public static Vector operator *(float f, Vector v)
        {
            return v * f;
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
