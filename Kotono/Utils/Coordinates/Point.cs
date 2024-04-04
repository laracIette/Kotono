using Kotono.Graphics;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Kotono.Utils.Coordinates
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IEquatable<Point>
    {
        /// <summary> 
        /// The X component of the Point. 
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The Y component of the Point. 
        /// </summary>
        public float Y { get; set; }

        /// <summary> 
        /// The length component of the Point. 
        /// </summary>
        [JsonIgnore]
        public readonly float Length => Math.Sqrt(X * X + Y * Y);

        /// <summary> 
        /// The Point scaled to unit length. 
        /// </summary>
        [JsonIgnore]
        public readonly Point Normalized => this / Length;

        /// <summary>
        /// The Point scaled to Normalized Device Coordinates.
        /// </summary>
        [JsonIgnore]
        public readonly Point NDC =>
            new Point(
                2.0f * X / WindowComponentManager.ActiveViewport.W - 1.0f,
                1.0f - Y / WindowComponentManager.ActiveViewport.H * 2.0f
            );

        /// <summary>
        /// The X / Y ratio of the Point.
        /// </summary>
        [JsonIgnore]
        public readonly float Ratio => X / Y;

        /// <summary>
        /// The X * Y product of the Point.
        /// </summary>
        [JsonIgnore]
        public readonly float Product => X * Y;

        public static Point Zero => new Point(0.0f, 0.0f);

        public static Point Unit => new Point(1.0f, 1.0f);

        public static Point UnitX => new Point(1.0f, 0.0f);

        public static Point UnitY => new Point(0.0f, 1.0f);

        public static int SizeInBytes => sizeof(float) * 2;

        public Point()
        {
            X = 0.0f;
            Y = 0.0f;
        }

        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public Point(float f)
        {
            X = f;
            Y = f;
        }

        public Point(float x = 0.0f, float y = 0.0f)
        {
            X = x;
            Y = y;
        }

        public static float Distance(Point left, Point right)
        {
            return (left - right).Length;
        }

        public static float Min(Point p)
        {
            return Math.Min(p.X, p.Y);
        }

        public static float Max(Point p)
        {
            return Math.Max(p.X, p.Y);
        }

        public static Point Clamp(Point p, float min, float max)
        {
            return Clamp(p, new Point(min), new Point(max));
        }

        public static Point Clamp(Point p, Point min, Point max)
        {
            p.X = Math.Clamp(p.X, min.X, max.X);
            p.Y = Math.Clamp(p.Y, min.Y, max.Y);
            return p;
        }

        public static Point operator +(Point left, Point right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }

        public static Point operator +(Point p, float f)
        {
            p.X += f;
            p.Y += f;
            return p;
        }

        public static Point operator -(Point left, Point right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        public static Point operator -(Point p)
        {
            p.X = -p.X;
            p.Y = -p.Y;
            return p;
        }

        public static Point operator *(Point left, Point right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            return left;
        }

        public static Point operator *(Point p, float f)
        {
            p.X *= f;
            p.Y *= f;
            return p;
        }

        public static Point operator /(Point left, Point right)
        {
            left.X /= right.X;
            left.Y /= right.Y;
            return left;
        }

        public static Point operator /(Point p, float f)
        {
            p.X /= f;
            p.Y /= f;
            return p;
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public static bool operator >(Point left, Point right)
        {
            return left.X > right.X
                && left.Y > right.Y;
        }

        public static bool operator <(Point left, Point right)
        {
            return left.X < right.X
                && left.Y < right.Y;
        }

        public static bool operator >=(Point left, Point right)
        {
            return left.X >= right.X
                && left.Y >= right.Y;
        }

        public static bool operator <=(Point left, Point right)
        {
            return left.X <= right.X
                && left.Y <= right.Y;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Point p && Equals(p);
        }

        public readonly bool Equals(Point other)
        {
            return X == other.X
                && Y == other.Y;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static implicit operator Point((float X, float Y) t)
        {
            return new Point(t.X, t.Y);
        }

        public static explicit operator PointI(Point p)
        {
            return new PointI((int)p.X, (int)p.Y);
        }

        public static explicit operator Vector2(Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static explicit operator Point(Vector2 v)
        {
            return new Point(v.X, v.Y);
        }

        public static explicit operator Vector2i(Point p)
        {
            return new Vector2i((int)p.X, (int)p.Y);
        }

        public static explicit operator Point(Vector2i v)
        {
            return new Point(v.X, v.Y);
        }

        public override readonly string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}
