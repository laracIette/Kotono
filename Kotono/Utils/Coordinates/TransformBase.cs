using System;

namespace Kotono.Utils.Coordinates
{
    public struct TransformBase(Vector location, Rotator rotation, Vector scale) : IEquatable<TransformBase>
    {
        public Vector Location { get; set; } = location;

        public Rotator Rotation { get; set; } = rotation;

        public Vector Scale { get; set; } = scale;

        public static TransformBase operator +(TransformBase left, in TransformBase right)
        {
            left.Location += right.Location;
            left.Rotation += right.Rotation;
            left.Scale += right.Scale - Vector.Unit;
            return left;
        }

        public static TransformBase operator *(in float f, TransformBase t)
        {
            t.Location = f * t.Location;
            t.Rotation = f * t.Rotation;
            t.Scale = Vector.Unit + f * (t.Scale - Vector.Unit);
            return t;
        }

        public static TransformBase operator /(TransformBase t, in float f)
        {
            t.Location /= f;
            t.Rotation /= f;
            t.Scale = Vector.Unit + (t.Scale - Vector.Unit) / f;
            return t;
        }

        public static bool operator ==(TransformBase left, TransformBase right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TransformBase left, TransformBase right)
        {
            return !(left == right);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is TransformBase transformBase && Equals(transformBase);
        }

        public readonly bool Equals(TransformBase other)
        {
            return other.Location == Location
                && other.Rotation == Rotation
                && other.Scale == Scale;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Location, Rotation, Scale);
        }
    }
}
