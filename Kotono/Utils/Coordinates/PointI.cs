using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct PointI : IEquatable<PointI>
    {
        /// <summary> 
        /// The X component of the PointI. 
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y component of the PointI. 
        /// </summary>
        public int Y { get; set; }

        /// <summary> 
        /// The length component of the PointI. 
        /// </summary>
        public readonly float Length => Math.Sqrt(X * X + Y * Y);

        /// <summary> 
        /// The PointI scaled to unit length. 
        /// </summary>
        public readonly PointI Normalized => this / Length;

        /// <summary>
        /// The X / Y ratio of the PointI.
        /// </summary>
        public readonly float Ratio => X / Y;

        /// <summary>
        /// The X * Y product of the PointI.
        /// </summary>
        public readonly int Product => X * Y;

        public static PointI Zero => new PointI(0, 0);

        public static PointI Unit => new PointI(1, 1);

        public static PointI UnitX => new PointI(1, 0);

        public static PointI UnitY => new PointI(0, 1);

        public static int SizeInBytes => sizeof(int) * 2;

        public PointI()
        {
            X = 0;
            Y = 0;
        }

        public PointI(PointI p)
        {
            X = p.X;
            Y = p.Y;
        }

        public PointI(int i)
        {
            X = i;
            Y = i;
        }

        public PointI(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public static float Distance(PointI left, PointI right)
        {
            return (left - right).Length;
        }

        public static int Min(PointI p)
        {
            return (int)Math.Min(p.X, p.Y);
        }

        public static int Max(PointI p)
        {
            return (int)Math.Max(p.X, p.Y);
        }

        public static PointI Clamp(PointI p, int min, int max)
        {
            return Clamp(p, new PointI(min), new PointI(max));
        }

        public static PointI Clamp(PointI p, PointI min, PointI max)
        {
            p.X = (int)Math.Clamp(p.X, min.X, max.X);
            p.Y = (int)Math.Clamp(p.Y, min.Y, max.Y);
            return p;
        }

        public static PointI operator +(PointI left, PointI right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }

        public static PointI operator +(PointI p, int i)
        {
            p.X += i;
            p.Y += i;
            return p;
        }

        public static PointI operator -(PointI left, PointI right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        public static PointI operator -(PointI p, int i)
        {
            p.X -= i;
            p.Y -= i;
            return p;
        }

        public static PointI operator -(PointI p)
        {
            p.X = -p.X;
            p.Y = -p.Y;
            return p;
        }

        public static PointI operator *(PointI left, PointI right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            return left;
        }

        public static PointI operator *(int i, PointI p)
        {
            p.X *= i;
            p.Y *= i;
            return p;
        }

        [Obsolete("Reorder operands, use \"PointI.operator *(int, PointI)\" instead.")]
        public static PointI operator *(PointI p, int i)
        {
            return i * p;
        }

        public static PointI operator /(PointI left, PointI right)
        {
            left.X /= right.X;
            left.Y /= right.Y;
            return left;
        }

        public static PointI operator /(PointI p, int i)
        {
            p.X /= i;
            p.Y /= i;
            return p;
        }

        public static PointI operator /(PointI p, float f)
        {
            p.X = (int)(p.X / f);
            p.Y = (int)(p.Y / f);
            return p;
        }

        public static bool operator ==(PointI left, PointI right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PointI left, PointI right)
        {
            return !(left == right);
        }

        public static bool operator >(PointI left, PointI right)
        {
            return left.X > right.X
                && left.Y > right.Y;
        }

        public static bool operator <(PointI left, PointI right)
        {
            return left.X < right.X
                && left.Y < right.Y;
        }

        public static bool operator >=(PointI left, PointI right)
        {
            return left.X >= right.X
                && left.Y >= right.Y;
        }

        public static bool operator <=(PointI left, PointI right)
        {
            return left.X <= right.X
                && left.Y <= right.Y;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is PointI p && Equals(p);
        }

        public readonly bool Equals(PointI other)
        {
            return X == other.X
                && Y == other.Y;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static implicit operator (int X, int Y)(PointI p)
        {
            return (p.X, p.Y);
        }

        public static implicit operator PointI((int X, int Y) t)
        {
            return new PointI(t.X, t.Y);
        }

        public static explicit operator Point(PointI p)
        {
            return new Point(p.X, p.Y);
        }

        public static explicit operator PointI(Vector2i v)
        {
            return new PointI(v.X, v.Y);
        }

        public static explicit operator Vector2i(PointI v)
        {
            return new Vector2i(v.X, v.Y);
        }

        public override readonly string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}
