using OpenTK.Mathematics;
using System;
using System.Text.Json.Serialization;

namespace Kotono.Utils.Coordinates
{
    public struct Rotator : IEquatable<Rotator>
    {
        /// <summary>
        /// The roll angle of the Rotator in degrees.
        /// </summary>
        public float Roll { get; set; }

        /// <summary>
        /// The pitch angle of the Rotator in degrees.
        /// </summary>
        public float Pitch { get; set; }

        /// <summary>
        /// The yaw angle of the Rotator in degrees.
        /// </summary>
        public float Yaw { get; set; }

        [JsonIgnore]
        public readonly Rotator Radians => new Rotator(Math.Rad(Roll), Math.Rad(Pitch), Math.Rad(Yaw));

        public static Rotator Zero => new Rotator(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// Initialize a <see cref="Rotator"/> from euler angles with [Roll = roll, Pitch = pitch, Yaw = yaw].
        /// </summary>
        public Rotator(float roll = 0.0f, float pitch = 0.0f, float yaw = 0.0f)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }

        /// <summary>
        /// Initialize a <see cref="Rotator"/> from euler angles with [Roll = 0, Pitch = 0, Yaw = 0].
        /// </summary>
        public Rotator() : this(0.0f, 0.0f, 0.0f) { }

        /// <summary>
        /// Initialize a <see cref="Rotator"/> from euler angles with [Roll = r.Roll, Pitch = r.Pitch, Yaw = r.Yaw].
        /// </summary>
        public Rotator(Rotator r) : this(r.Roll, r.Pitch, r.Yaw) { }

        /// <summary>
        /// Initialize a <see cref="Rotator"/> from euler angles with [Roll = f, Pitch = f, Yaw = f].
        /// </summary>
        public Rotator(float f) : this(f, f, f) { }

        /// <summary>
        /// Get a <see cref="Rotator"/> from radian angles with [Roll = roll, Pitch = pitch, Yaw = yaw].
        /// </summary>
        public static Rotator FromRadians(float roll = 0.0f, float pitch = 0.0f, float yaw = 0.0f)
        {
            return new Rotator(Math.Deg(roll), Math.Deg(pitch), Math.Deg(yaw));
        }

        public static Rotator operator +(Rotator left, Rotator right)
        {
            left.Roll += right.Roll;
            left.Pitch += right.Pitch;
            left.Yaw += right.Yaw;
            return left;
        }

        public static Rotator operator -(Rotator left, Rotator right)
        {
            left.Roll -= right.Roll;
            left.Pitch -= right.Pitch;
            left.Yaw -= right.Yaw;
            return left;
        }

        public static Rotator operator -(Rotator v, float f)
        {
            v.Roll -= f;
            v.Pitch -= f;
            v.Yaw -= f;
            return v;
        }

        public static Rotator operator -(Rotator v)
        {
            v.Roll = -v.Roll;
            v.Pitch = -v.Pitch;
            v.Yaw = -v.Yaw;
            return v;
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
            return Roll == other.Roll
                && Pitch == other.Pitch
                && Yaw == other.Yaw;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Roll, Pitch, Yaw);
        }

        public static explicit operator Vector3(Rotator r)
        {
            return new Vector3(r.Roll, r.Pitch, r.Yaw);
        }

        public static explicit operator Rotator(Vector3 r)
        {
            return new Rotator(r.X, r.Y, r.Z);
        }

        public override readonly string ToString()
        {
            return $"Roll: {Roll}, Pitch: {Pitch}, Yaw: {Yaw}";
        }
    }
}
