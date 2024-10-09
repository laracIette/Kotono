using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct PointI : IEquatable<PointI>
    {
        /// <summary> 
        /// The X component of the <see cref="PointI"/>. 
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The Y component of the <see cref="PointI"/>. 
        /// </summary>
        public readonly int Y;

        /// <summary> 
        /// The length component of the <see cref="PointI"/>. 
        /// </summary>
        public readonly float Length => Math.Sqrt(X * X + Y * Y);

        /// <summary> 
        /// The PointI scaled to unit length. 
        /// </summary>
        public readonly PointI Normalized => this / Length;

        /// <summary>
        /// The X / Y ratio of the <see cref="PointI"/>.
        /// </summary>
        public readonly float Ratio => X / (float)Y;

        /// <summary>
        /// The X * Y product of the <see cref="PointI"/>.
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
            => (left - right).Length;

        /// <summary> 
        /// Get the absolute values of the <see cref="PointI"/>. 
        /// </summary>
        public static PointI Abs(PointI p)
            => new(Math.Abs(p.X), Math.Abs(p.Y));

        /// <summary> 
        /// Get the half of the values of the <see cref="PointI"/>. 
        /// </summary>
        public static PointI Half(PointI p)
            => new(Math.Half(p.X), Math.Half(p.Y));

        public static int Min(PointI p)
            => Math.Min(p.X, p.Y);

        public static int Max(PointI p) 
            => Math.Max(p.X, p.Y);

        public static PointI Clamp(PointI p, int min, int max) 
            => Clamp(p, new PointI(min), new PointI(max));

        public static PointI Clamp(PointI p, PointI min, PointI max)
            => new(Math.Clamp(p.X, min.X, max.X), Math.Clamp(p.Y, min.Y, max.Y));

        public static bool IsZero(PointI p) 
            => p == Zero;

        public static PointI operator +(PointI left, PointI right) 
            => new(left.X + right.X, left.Y + right.Y);

        public static PointI operator +(int i, PointI p) 
            => new(p.X + i, p.Y + i);

        [Obsolete("Reorder operands, use 'PointI.operator +(int, PointI)' instead.")]
        public static PointI operator +(PointI p, int i) 
            => i + p;

        public static PointI operator -(PointI left, PointI right)
            => new(left.X - right.X, left.Y - right.Y);

        public static PointI operator -(PointI p, int i) 
            => new(p.X - i, p.Y - i);

        public static PointI operator -(PointI p) 
            => new(-p.X, -p.Y);

        public static PointI operator *(PointI left, PointI right) 
            => new(left.X * right.X, left.Y * right.Y);

        public static PointI operator *(int i, PointI p) 
            => new(p.X * i, p.Y * i);

        [Obsolete("Reorder operands, use 'PointI.operator *(int, PointI)' instead.")]
        public static PointI operator *(PointI p, int i) 
            => i * p;

        public static PointI operator /(PointI left, PointI right)
            => new(left.X / right.X, left.Y / right.Y);

        public static PointI operator /(PointI p, int i) 
            => new(p.X / i, p.Y / i);

        public static PointI operator /(PointI p, float f) 
            => new((int)(p.X / f), (int)(p.Y / f));

        public static bool operator ==(PointI left, PointI right) 
            => left.Equals(right);

        public static bool operator !=(PointI left, PointI right)
            => !(left == right);

        public static bool operator >(PointI left, PointI right)
            => left.X > right.X
            && left.Y > right.Y;

        public static bool operator <(PointI left, PointI right) 
            => left.X < right.X
            && left.Y < right.Y;

        public static bool operator >=(PointI left, PointI right)
            => left.X >= right.X
            && left.Y >= right.Y;

        public static bool operator <=(PointI left, PointI right)
            => left.X <= right.X
            && left.Y <= right.Y;

        public readonly void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public override readonly bool Equals(object? obj) 
            => obj is PointI p && Equals(p);

        public readonly bool Equals(PointI other) 
            => X == other.X
            && Y == other.Y;

        public override readonly int GetHashCode() 
            => HashCode.Combine(X, Y);

        public static implicit operator (int X, int Y)(PointI p)
            => (p.X, p.Y);

        public static implicit operator PointI((int X, int Y) t)
            => new(t.X, t.Y);

        public static implicit operator Point(PointI p) 
            => new(p.X, p.Y);

        public static implicit operator PointI(Vector2i v) 
            => new(v.X, v.Y);

        public static implicit operator Vector2i(PointI v) 
            => new(v.X, v.Y);

        public override readonly string ToString() 
            => $"X: {X}, Y: {Y}";
    }
}
