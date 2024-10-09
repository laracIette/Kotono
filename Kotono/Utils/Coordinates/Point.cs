using Assimp;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Point : IEquatable<Point>
    {
        /// <summary> 
        /// The X component of the <see cref="Point"/>. 
        /// </summary>
        public readonly float X;

        /// <summary>
        /// The Y component of the <see cref="Point"/>. 
        /// </summary>
        public readonly float Y;

        /// <summary> 
        /// The length component of the <see cref="Point"/>. 
        /// </summary>
        public readonly float Length => Math.Sqrt(X * X + Y * Y);

        /// <summary> 
        /// The Point scaled to unit length. 
        /// </summary>
        public readonly Point Normalized => this / Length;

        /// <summary>
        /// The Point scaled to Normalized Device Coordinates.
        /// </summary>
        public readonly Point NDC => new(
            2.0f * X / Window.Viewport.Size.X - 1.0f,
            1.0f - Y / Window.Viewport.Size.Y * 2.0f
        );

        /// <summary>
        /// The X / Y ratio of the <see cref="Point"/>.
        /// </summary>
        public readonly float Ratio => X / Y;

        /// <summary>
        /// The X * Y product of the <see cref="Point"/>.
        /// </summary>
        public readonly float Product => X * Y;

        public static Point Zero => new(0.0f, 0.0f);

        public static Point Unit => new(1.0f, 1.0f);

        public static Point UnitX => new(1.0f, 0.0f);

        public static Point UnitY => new(0.0f, 1.0f);

        public static int SizeInBytes => sizeof(float) * 2;

        /// <summary> 
        /// Initialize a <see cref="Point"/> with X = x, Y = y.
        /// </summary>
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary> 
        /// Initialize a <see cref="Point"/> with X = 0, Y = 0.
        /// </summary>
        public Point() : this(0.0f, 0.0f) { }

        /// <summary> 
        /// Initialize a <see cref="Point"/> with X = f, Y = f.
        /// </summary>
        public Point(float f) : this(f, f) { }

        public static float Distance(Point left, Point right) 
            => (left - right).Length;

        /// <summary> 
        /// Get the absolute values of the <see cref="Point"/>. 
        /// </summary>
        public static Point Abs(Point p)
            => new(Math.Abs(p.X), Math.Abs(p.Y));

        /// <summary> 
        /// Get the half of the values of the <see cref="Point"/>. 
        /// </summary>
        public static Point Half(Point p) 
            => new(Math.Half(p.X), Math.Half(p.Y));

        public static float Min(Point p) 
            => Math.Min(p.X, p.Y);

        public static float Max(Point p)
            => Math.Max(p.X, p.Y);

        public static Point Clamp(Point p, float min, float max) 
            => Clamp(p, new Point(min), new Point(max));

        public static Point Clamp(Point p, Point min, Point max)
            => new(Math.Clamp(p.X, min.X, max.X), Math.Clamp(p.Y, min.Y, max.Y));

        public static bool IsZero(Point p)
            => p == Zero;

        public static Point operator +(Point left, Point right)
            => new(left.X + right.X, left.Y + right.Y);

        public static Point operator +(float f, Point p) 
            => new(p.X + f, p.Y + f);

        [Obsolete("Reorder operands, use 'Point.operator +(float, Point)' instead.")]
        public static Point operator +(Point p, float f)
            => f + p;

        public static Point operator -(Point left, Point right)
            => new(left.X - right.X, left.Y - right.Y);

        public static Point operator -(Point p, float f) 
            => new(p.X - f, p.Y - f);

        public static Point operator -(Point p) 
            => new(-p.X, -p.Y);

        public static Point operator *(Point left, Point right) 
            => new(left.X * right.X, left.Y * right.Y);

        public static Point operator *(float f, Point p) 
            => new(p.X * f, p.Y * f);

        [Obsolete("Reorder operands, use 'Point.operator *(float, Point)' instead.")]
        public static Point operator *(Point p, float f) 
            => f * p;

        public static Point operator /(Point left, Point right) 
            => new(left.X / right.X, left.Y / right.Y);

        public static Point operator /(Point p, float f) 
            => new(p.X / f, p.Y / f);

        public static bool operator ==(Point left, Point right) 
            => left.Equals(right);

        public static bool operator !=(Point left, Point right) 
            => !(left == right);

        public static bool operator >(Point left, Point right)
            => left.X > right.X
            && left.Y > right.Y;

        public static bool operator <(Point left, Point right) 
            => left.X < right.X
            && left.Y < right.Y;

        public static bool operator >=(Point left, Point right) 
            => left.X >= right.X
            && left.Y >= right.Y;

        public static bool operator <=(Point left, Point right) 
            => left.X <= right.X
            && left.Y <= right.Y;

        public readonly void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        public override readonly bool Equals(object? obj)
            => obj is Point p && Equals(p);

        public readonly bool Equals(Point other) 
            => X == other.X
            && Y == other.Y;

        public override readonly int GetHashCode()
            => HashCode.Combine(X, Y);

        public static implicit operator Point((float X, float Y) t) 
            => new(t.X, t.Y);

        public static explicit operator PointI(Point p) 
            => new((int)p.X, (int)p.Y);

        public static implicit operator Vector2(Point p) 
            => new(p.X, p.Y);

        public static implicit operator Point(Vector2 v) 
            => new(v.X, v.Y);

        public static explicit operator Vector2i(Point p) 
            => new((int)p.X, (int)p.Y);

        public static implicit operator Point(Vector2i v) 
            => new(v.X, v.Y);

        public static implicit operator Vector3D(Point p)
            => new(p.X, p.Y, 0.0f);

        public static implicit operator Point(Vector3D v)
            => new(v.X, v.Y);

        public override readonly string ToString() 
            => $"X: {X}, Y: {Y}";
    }
}
