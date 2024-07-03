using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rotator : IEquatable<Rotator>
    {
        /// <summary>
        /// The pitch angle of the <see cref="Rotator"/> in degrees.
        /// </summary>
        [JsonInclude]
        public float Pitch = 0.0f;

        /// <summary>
        /// The yaw angle of the <see cref="Rotator"/> in degrees.
        /// </summary>
        [JsonInclude]
        public float Yaw = 0.0f;

        /// <summary>
        /// The roll angle of the <see cref="Rotator"/> in degrees.
        /// </summary>
        [JsonInclude]
        public float Roll = 0.0f;

        /// <summary>
        /// The <see cref="Rotator"/> in radians.
        /// </summary>
        public readonly Rotator Degrees => new(Math.Deg(Pitch), Math.Deg(Yaw), Math.Deg(Roll));

        /// <summary>
        /// The rotation matrix of the <see cref="Rotator"/>.
        /// </summary>
        public readonly Matrix4 RotationMatrix => Matrix4.CreateFromQuaternion((Quaternion)this);

        /// <summary>
        /// Wether the <see cref="Rotator"/> is equal to <see cref="Zero"/>.
        /// </summary>
        public readonly bool IsZero => this == Zero;

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = 0, Yaw = 0, Roll = 0.
        /// </summary>
        public static Rotator Zero => new(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = <see cref="Math.TAU"/>, Yaw = <see cref="Math.TAU"/>, Roll = <see cref="Math.TAU"/>.
        /// </summary>
        public static Rotator Unit => new(Math.TAU, Math.TAU, Math.TAU);

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = <see cref="Math.TAU"/>, Yaw = 0, Roll = 0.
        /// </summary>
        public static Rotator UnitPitch => new(Math.TAU, 0.0f, 0.0f);

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = 0, Yaw = <see cref="Math.TAU"/>, Roll = 0.
        /// </summary>
        public static Rotator UnitYaw => new(0.0f, Math.TAU, 0.0f);

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = 0, Yaw = 0, Roll = <see cref="Math.TAU"/>.
        /// </summary>
        public static Rotator UnitRoll => new(0.0f, 0.0f, Math.TAU);

        /// <summary>
        /// Initialize a <see cref="Rotator"/> from radians with Pitch = pitch, Yaw = yaw, Roll = roll.
        /// </summary>
        public Rotator(float pitch = 0.0f, float yaw = 0.0f, float roll = 0.0f)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }

        /// <summary>
        /// Initialize a <see cref="Rotator"/> from radians with Pitch = 0, Yaw = 0, Roll = 0.
        /// </summary>
        public Rotator() : this(0.0f, 0.0f, 0.0f) { }

        /// <summary>
        /// Initialize a <see cref="Rotator"/> with Pitch = r.Pitch, Yaw = r.Yaw, Roll = r.Roll.
        /// </summary>
        public Rotator(Rotator r) : this(r.Pitch, r.Yaw, r.Roll) { }

        /// <summary>
        /// Initialize a <see cref="Rotator"/> from radians with Pitch = f, Yaw = f, Roll = f.
        /// </summary>
        public Rotator(float f) : this(f, f, f) { }

        /// <summary>
        /// Get a <see cref="Rotator"/> from degrees with Pitch = pitch, Yaw = yaw, Roll = roll.
        /// </summary>
        public static Rotator FromDegrees(float pitch = 0.0f, float yaw = 0.0f, float roll = 0.0f)
        {
            return new Rotator(Math.Rad(pitch), Math.Rad(yaw), Math.Rad(roll));
        }

        public static Rotator operator +(Rotator left, Rotator right)
        {
            left.Pitch += right.Pitch;
            left.Yaw += right.Yaw;
            left.Roll += right.Roll;
            return left;
        }

        public static Rotator operator -(Rotator left, Rotator right)
        {
            left.Pitch -= right.Pitch;
            left.Yaw -= right.Yaw;
            left.Roll -= right.Roll;
            return left;
        }

        public static Rotator operator -(Rotator r, float f)
        {
            r.Pitch -= f;
            r.Yaw -= f;
            r.Roll -= f;
            return r;
        }

        public static Rotator operator -(Rotator r)
        {
            r.Pitch = -r.Pitch;
            r.Yaw = -r.Yaw;
            r.Roll = -r.Roll;
            return r;
        }

        public static Rotator operator *(float f, Rotator r)
        {
            r.Pitch *= f;
            r.Yaw *= f;
            r.Roll *= f;
            return r;
        }

        [Obsolete("Reorder operands, use \"Rotator.operator *(float, Rotator)\" instead.")]
        public static Rotator operator *(Rotator r, float f)
        {
            return f * r;
        }

        public static Rotator operator /(Rotator r, float f)
        {
            r.Pitch /= f;
            r.Yaw /= f;
            r.Roll /= f;
            return r;
        }

        public static bool operator ==(Rotator left, Rotator right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Rotator left, Rotator right)
        {
            return !(left == right);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Rotator v && Equals(v);
        }

        public readonly bool Equals(Rotator other)
        {
            return Pitch == other.Pitch
                && Yaw == other.Yaw
                && Roll == other.Roll;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Pitch, Yaw, Roll);
        }

        public static explicit operator Quaternion(Rotator r)
        {
            return new Quaternion(r.Pitch, r.Yaw, r.Roll);
        }

        public static explicit operator Rotator(Quaternion q)
        {
            return new Rotator(q.X, q.Y, q.Z);
        }

        public static explicit operator Vector3(Rotator r)
        {
            return new Vector3(r.Pitch, r.Yaw, r.Roll);
        }

        public static explicit operator Rotator(Vector3 r)
        {
            return new Rotator(r.X, r.Y, r.Z);
        }

        public override readonly string ToString()
        {
            return $"Pitch: {Pitch}, Yaw: {Yaw}, Roll: {Roll}";
        }
    }
}
