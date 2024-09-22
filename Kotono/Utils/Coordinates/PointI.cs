using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct PointI : IEquatable<PointI>
    {
        /// <summary> 
        /// The X component of the PointI. 
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The Y component of the PointI. 
        /// </summary>
        public readonly int Y;

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
        public readonly float Ratio => X / (float)Y;

        /// <summary>
        /// The X * Y product of the PointI.
        /// </summary>
        public readonly int Product => X * Y;

        public static PointI Zero => new(0, 0);

        public static PointI Unit => new(1, 1);

        public static PointI UnitX => new(1, 0);

        public static PointI UnitY => new(0, 1);

        public static int SizeInBytes => sizeof(int) * 2;

        /// <summary> 
        /// Initialize a <see cref="PointI"/> with X = x, Y = y.
        /// </summary>
        public PointI(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary> 
        /// Initialize a <see cref="PointI"/> with X = 0, Y = 0.
        /// </summary>
        public PointI() : this(0, 0) { }

        /// <summary> 
        /// Initialize a <see cref="PointI"/> with X = f, Y = f.
        /// </summary>
        public PointI(int f) : this(f, f) { }

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
            return new PointI((int)Math.Clamp(p.X, min.X, max.X), (int)Math.Clamp(p.Y, min.Y, max.Y));
        }

        public static bool IsNullOrZero(PointI? p)
        {
            return p is null || p == Zero;
        }

        public static PointI operator +(PointI left, PointI right)
        {
            return new PointI(left.X + right.X, left.Y + right.Y);
        }

        public static PointI operator +(int i, PointI p)
        {
            return new PointI(p.X + i, p.Y + i);
        }

        [Obsolete("Reorder operands, use 'PointI.operator +(int, PointI)' instead.")]
        public static PointI operator +(PointI p, int i)
        {
            return i + p;
        }

        public static PointI operator -(PointI left, PointI right)
        {
            return new PointI(left.X - right.X, left.Y - right.Y);
        }

        public static PointI operator -(PointI p, int i)
        {
            return new PointI(p.X - i, p.Y - i);
        }

        public static PointI operator -(PointI p)
        {
            return new PointI(-p.X, -p.Y);
        }

        public static PointI operator *(PointI left, PointI right)
        {
            return new PointI(left.X * right.X, left.Y * right.Y);
        }

        public static PointI operator *(int i, PointI p)
        {
            return new PointI(p.X * i, p.Y * i);
        }

        [Obsolete("Reorder operands, use 'PointI.operator *(int, PointI)' instead.")]
        public static PointI operator *(PointI p, int i)
        {
            return i * p;
        }

        public static PointI operator /(PointI left, PointI right)
        {
            return new PointI(left.X / right.X, left.Y / right.Y);
        }

        public static PointI operator /(PointI p, int i)
        {
            return new PointI(p.X / i, p.Y / i);
        }

        public static PointI operator /(PointI p, float f)
        {
            return new PointI((int)(p.X / f), (int)(p.Y / f));
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

        public readonly void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
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
