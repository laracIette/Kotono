using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Rotator : IEquatable<Rotator>
    {
        /// <summary>
        /// The pitch angle of the <see cref="Rotator"/> in degrees.
        /// </summary>
        public readonly float Pitch;

        /// <summary>
        /// The yaw angle of the <see cref="Rotator"/> in degrees.
        /// </summary>
        public readonly float Yaw;

        /// <summary>
        /// The roll angle of the <see cref="Rotator"/> in degrees.
        /// </summary>
        public readonly float Roll;

        /// <summary>
        /// The <see cref="Rotator"/> in radians.
        /// </summary>
        public readonly Rotator Degrees => new(Math.Deg(Pitch), Math.Deg(Yaw), Math.Deg(Roll));

        /// <summary>
        /// The rotation matrix of the <see cref="Rotator"/>.
        /// </summary>
        public readonly Matrix4 RotationMatrix => Matrix4.CreateFromQuaternion((Quaternion)this);

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = 0, Yaw = 0, Roll = 0.
        /// </summary>
        public static Rotator Zero => new(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = <see cref="Math.Tau"/>, Yaw = <see cref="Math.Tau"/>, Roll = <see cref="Math.Tau"/>.
        /// </summary>
        public static Rotator Unit => new(Math.Tau, Math.Tau, Math.Tau);

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = <see cref="Math.Tau"/>, Yaw = 0, Roll = 0.
        /// </summary>
        public static Rotator UnitPitch => new(Math.Tau, 0.0f, 0.0f);

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = 0, Yaw = <see cref="Math.Tau"/>, Roll = 0.
        /// </summary>
        public static Rotator UnitYaw => new(0.0f, Math.Tau, 0.0f);

        /// <summary>
        /// A <see cref="Rotator"/> with Pitch = 0, Yaw = 0, Roll = <see cref="Math.Tau"/>.
        /// </summary>
        public static Rotator UnitRoll => new(0.0f, 0.0f, Math.Tau);

        /// <summary>
        /// Initialize a <see cref="Rotator"/> from radians with Pitch = pitch, Yaw = yaw, Roll = roll.
        /// </summary>
        public Rotator(float pitch, float yaw, float roll)
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

        public static bool IsNullOrZero(Rotator? r)
        {
            return r is null || r == Zero;
        }

        public static Rotator operator +(Rotator left, Rotator right)
        {
            return new Rotator(left.Pitch + right.Pitch, left.Yaw + right.Yaw, left.Roll + right.Roll);
        }

        public static Rotator operator -(Rotator left, Rotator right)
        {
            return new Rotator(left.Pitch - right.Pitch, left.Yaw - right.Yaw, left.Roll - right.Roll);
        }

        public static Rotator operator -(Rotator r, float f)
        {
            return new Rotator(r.Pitch - f, r.Yaw - f, r.Roll - f);
        }

        public static Rotator operator -(Rotator r)
        {
            return new Rotator(-r.Pitch, -r.Yaw, -r.Roll);
        }

        public static Rotator operator *(float f, Rotator r)
        {
            return new Rotator(r.Pitch * f, r.Yaw * f, r.Roll * f);
        }


        [Obsolete("Reorder operands, use \"Rotator.operator *(float, Rotator)\" instead.")]
        public static Rotator operator *(Rotator r, float f)
        {
            return f * r;
        }

        public static Rotator operator /(Rotator r, float f)
        {
            return new Rotator(r.Pitch / f, r.Yaw / f, r.Roll / f);
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
