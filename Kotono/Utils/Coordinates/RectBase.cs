using System;

namespace Kotono.Utils.Coordinates
{
    public struct RectBase : IEquatable<RectBase>
    {
        public Point BaseSize { get; set; } = Point.Zero;

        public Point Position { get; set; } = Point.Zero;

        public Rotator Rotation { get; set; } = Rotator.Zero;

        public Point Scale { get; set; } = Point.Unit;

        public Point Size
        {
            readonly get => BaseSize * Scale;
            set
            {
                if (Point.IsNullOrZero(BaseSize))
                {
                    BaseSize = value / Scale;
                }
                else
                {
                    Scale = value / BaseSize;
                }
            }
        }

        public RectBase(Point position, Point baseSize, Point size, Rotator rotation)
        {
            Position = position;
            BaseSize = baseSize;
            Size = size;
            Rotation = rotation;
        }

        public RectBase(Point position, Point baseSize, Rotator rotation, Point scale)
        {
            Position = position;
            BaseSize = baseSize;
            Rotation = rotation;
            Scale = scale;
        }

        public RectBase(Point position, Point size, Rotator rotation)
        {
            Position = position;
            BaseSize = size;
            Rotation = rotation;
        }

        public RectBase(Point position, Point size)
        {
            Position = position;
            BaseSize = size;
        }

        public static RectBase operator +(RectBase left, in RectBase right)
        {
            left.Position += right.Position;
            left.Scale += right.Scale - Point.Unit;
            return left;
        }

        public static RectBase operator *(in float f, RectBase t)
        {
            t.Position = f * t.Position;
            t.Scale = Point.Unit + f * (t.Scale - Point.Unit);
            return t;
        }

        public static RectBase operator /(RectBase t, in float f)
        {
            t.Position /= f;
            t.Scale = Point.Unit + (t.Scale - Point.Unit) / f;
            return t;
        }

        public static bool operator ==(RectBase left, RectBase right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RectBase left, RectBase right)
        {
            return !(left == right);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is RectBase rectBase && Equals(rectBase);
        }

        public readonly bool Equals(RectBase other)
        {
            return other.BaseSize == BaseSize
                && other.Position == Position
                && other.Rotation == Rotation
                && other.Scale == Scale;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(BaseSize, Position, Rotation, Scale);
        }
    }
}
