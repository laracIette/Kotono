using OpenTK.Mathematics;
using System;

namespace Kotono.Utils
{
    public struct Point
    {
        public float X;

        public float Y;

        public readonly float Length => MathF.Sqrt(X * X + Y * Y);

        public readonly Point Normalized => this / Length;

        public static Point Zero => new Point(0, 0);

        public static Point Unit => new Point(1, 1);

        public static Point UnitX => new Point(1, 0);

        public static Point UnitY => new Point(0, 1);

        public const int SizeInBytes = sizeof(float) * 2;

        public readonly Point WorldSpace =>
            new Point(
                2 * X / KT.ActiveViewport.W - 1,
                1 - 2 * Y / KT.ActiveViewport.H
            );

        public Point()
        {
            X = 0;
            Y = 0;
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
        
        public Point(double f)
        {
            X = (float)f;
            Y = (float)f;
        }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Point(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point left, Point right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
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
            return (left.X == right.X) && (left.Y == right.Y);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public static explicit operator Vector2(Point v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static explicit operator Point(Vector2 v)
        {
            return new Point(v.X, v.Y);
        }

        public static explicit operator Vector2i(Point v)
        {
            return new Vector2i((int)v.X, (int)v.Y);
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
