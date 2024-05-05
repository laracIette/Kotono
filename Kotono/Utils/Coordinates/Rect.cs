using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect : IEquatable<Rect>
    {
        /// <summary>
        /// The position component of the Rect.
        /// </summary>
        [JsonIgnore]
        public Point Position;

        /// <summary> 
        /// The size component of the Rect. 
        /// </summary>
        [JsonIgnore]
        public Point Size;

        /// <summary> 
        /// The X component of the Rect. 
        /// </summary>
        public float X
        {
            readonly get => Position.X;
            set => Position.X = value;
        }

        /// <summary> 
        /// The Y component of the Rect. 
        /// </summary>
        public float Y
        {
            readonly get => Position.Y;
            set => Position.Y = value;
        }

        /// <summary>
        /// The width component of the Rect. 
        /// </summary>
        public float W
        {
            readonly get => Size.X;
            set => Size.X = value;
        }

        /// <summary>
        /// The height component of the Rect.
        /// </summary>
        public float H
        {
            readonly get => Size.Y;
            set => Size.Y = value;
        }

        /// <summary> 
        /// The Rect scaled to unit length. 
        /// </summary>
        public readonly Rect Normalized =>
            new Rect(
                Position.Normalized,
                Size.Normalized
            );

        /// <summary>
        /// The Rect scaled to Normalized Device Coordinates.
        /// </summary>
        public readonly Rect NDC =>
            new Rect(
                Position.NDC,
                Size / WindowComponentManager.ActiveViewport.Size
            );

        /// <summary>
        /// The model matrix of the Rect.
        /// </summary>
        public readonly Matrix4 Model =>
            Matrix4.CreateScale(NDC.W, NDC.H, 1.0f)
            * Matrix4.CreateTranslation(NDC.X, NDC.Y, 0.0f);

        /// <summary>
        /// The center Point of the Rect.
        /// </summary>
        public readonly Point Center => Position;

        /// <summary>
        /// The left Point of the Rect.
        /// </summary>
        public readonly Point Left => new Point(X - W / 2.0f, Y);

        /// <summary>
        /// The right Point of the Rect.
        /// </summary>
        public readonly Point Right => new Point(X + W / 2.0f, Y);

        /// <summary>
        /// The top Point of the Rect.
        /// </summary>
        public readonly Point Top => new Point(X, Y + H / 2.0f);

        /// <summary>
        /// The bottom Point of the Rect.
        /// </summary>
        public readonly Point Bottom => new Point(X, Y - H / 2.0f);

        /// <summary>
        /// The top left Point of the Rect.
        /// </summary>
        public readonly Point TopLeft => new Point(X - W / 2.0f, Y + H / 2.0f);

        /// <summary>
        /// The top right Point of the Rect.
        /// </summary>
        public readonly Point TopRight => new Point(X + W / 2.0f, Y + H / 2.0f);

        /// <summary>
        /// The bottom left Point of the Rect.
        /// </summary>
        public readonly Point BottomLeft => new Point(X - W / 2.0f, Y - H / 2.0f);

        /// <summary>
        /// The bottom right Point of the Rect.
        /// </summary>
        public readonly Point BottomRight => new Point(X + W / 2.0f, Y - H / 2.0f);

        /// <summary> 
        /// A Rect with X = 0, Y = 0, W = 0, H = 0.
        /// </summary>
        public static Rect Zero => new Rect(0.0f, 0.0f, 0.0f, 0.0f);

        /// <summary> 
        /// A Rect with X = 1, Y = 1, W = 1, H = 1. 
        /// </summary>
        public static Rect Unit => new Rect(1.0f, 1.0f, 1.0f, 1.0f);

        /// <summary> 
        /// A Rect with X = 1, Y = 0, W = 0, H = 0. 
        /// </summary>
        public static Rect UnitX => new Rect(1.0f, 0.0f, 0.0f, 0.0f);

        /// <summary> 
        /// A Rect with X = 0, Y = 1, W = 0, H = 0.
        /// </summary>
        public static Rect UnitY => new Rect(0.0f, 1.0f, 0.0f, 0.0f);

        /// <summary>
        /// A Rect with X = 0, Y = 0, W = 1, H = 0.
        /// </summary>
        public static Rect UnitW => new Rect(0.0f, 0.0f, 1.0f, 0.0f);

        /// <summary> 
        /// A Rect with X = 0, Y = 0, W = 0, H = 1.
        /// </summary>
        public static Rect UnitH => new Rect(0.0f, 0.0f, 0.0f, 1.0f);

        public static int SizeInBytes => Point.SizeInBytes * 2;

        public Rect()
        {
            X = 0.0f;
            Y = 0.0f;
            W = 0.0f;
            H = 0.0f;
        }

        public Rect(Rect r)
        {
            X = r.X;
            Y = r.Y;
            W = r.W;
            H = r.H;
        }

        public Rect(float f)
        {
            X = f;
            Y = f;
            W = f;
            H = f;
        }

        public Rect(Point position, Point size)
        {
            X = position.X;
            Y = position.Y;
            W = size.X;
            H = size.Y;
        }

        public Rect(Point position, float w, float h)
        {
            X = position.X;
            Y = position.Y;
            W = w;
            H = h;
        }

        public Rect(float x, float y, Point size)
        {
            X = x;
            Y = y;
            W = size.X;
            H = size.Y;
        }

        public Rect(float x = 0.0f, float y = 0.0f, float w = 0.0f, float h = 0.0f)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public static Rect FromAnchor(Rect r, Anchor a, Point offset)
        {
            r.Position += a switch
            {
                Anchor.Center => offset,
                Anchor.Left => new Point(r.W / 2.0f + offset.X, offset.Y),
                Anchor.Right => new Point(-r.W / 2.0f - offset.X, offset.Y),
                Anchor.Top => new Point(offset.X, r.H / 2.0f + offset.Y),
                Anchor.Bottom => new Point(offset.X, -r.H / 2.0f - offset.Y),
                Anchor.TopLeft => new Point(r.W / 2.0f + offset.X, r.H / 2.0f + offset.Y),
                Anchor.TopRight => new Point(-r.W / 2.0f - offset.X, r.H / 2.0f + offset.Y),
                Anchor.BottomLeft => new Point(r.W / 2.0f + offset.X, -r.H / 2.0f - offset.Y),
                Anchor.BottomRight => new Point(-r.W / 2.0f - offset.X, -r.H / 2.0f - offset.Y),
                _ => throw new Exception($"error: Rect.FromAnchor() doesn't handle \"{a}\"")
            };

            return r;
        }

        /// <summary> 
        /// Creates a Rect given a Rect and an Anchor.
        /// </summary>
        public static Rect FromAnchor(Rect r, Anchor a, float offset = 0.0f)
        {
            return FromAnchor(r, a, new Point(offset));
        }

        public static Rect[] FromAnchor(int n, Rect r, Anchor a, Point offset)
        {
            var result = Enumerable.Repeat(FromAnchor(r, a, offset), n).ToArray();

            for (int i = 0; i < n; i++)
            {
                result[i].Y = a switch
                {
                    Anchor.Center or Anchor.Left or Anchor.Right => r.Y - r.H / 2.0f * (n - 1) + r.H * i,
                    Anchor.Top or Anchor.TopLeft or Anchor.TopRight => throw new NotImplementedException(),
                    Anchor.Bottom or Anchor.BottomLeft or Anchor.BottomRight => throw new NotImplementedException(),
                    _ => throw new SwitchException(typeof(Anchor), a)
                };
            }

            return result;
        }

        /// <summary>
        /// Creates an array of Rect given a number of elements, a Rect and an Anchor.
        /// </summary>
        public static Rect[] FromAnchor(int n, Rect r, Anchor a, float offset = 0.0f)
        {
            return FromAnchor(n, r, a, new Point(offset));
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        public static bool Overlaps(Rect left, Rect right)
        {
            return Math.Abs(left.X - right.X) < (left.W + right.W) / 2.0f
                && Math.Abs(left.Y - right.Y) < (left.H + right.H) / 2.0f;
        }

        /// <summary> 
        /// Checks if r is overlapping with p.
        /// </summary>
        public static bool Overlaps(Rect r, Point p)
        {
            return Math.Abs(r.X - p.X) < r.W / 2.0f
                && Math.Abs(r.Y - p.Y) < r.H / 2.0f;
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        internal static bool Overlaps(Image left, Image right)
        {
            return Overlaps(left.Dest, right.Dest);
        }

        public static Rect Parse(string[] values)
        {
            return new Rect
            {
                X = float.Parse(values[0]),
                Y = float.Parse(values[1]),
                W = float.Parse(values[2]),
                H = float.Parse(values[3])
            };
        }

        public static Rect operator +(Rect left, Rect right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.W += right.W;
            left.H += right.H;
            return left;
        }

        public static Rect operator -(Rect left, Rect right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.W -= right.W;
            left.H -= right.H;
            return left;
        }

        public static Rect operator -(Rect r)
        {
            r.X += -r.X;
            r.Y += -r.Y;
            r.W += -r.W;
            r.H += -r.H;
            return r;
        }

        public static Rect operator *(Rect left, Rect right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.W *= right.W;
            left.H *= right.H;
            return left;
        }

        public static Rect operator *(Rect r, float value)
        {
            r.X *= value;
            r.Y *= value;
            r.W *= value;
            r.H *= value;
            return r;
        }

        public static Rect operator /(Rect left, Rect right)
        {
            left.X /= right.X;
            left.Y /= right.Y;
            left.W /= right.W;
            left.H /= right.H;
            return left;
        }

        public static Rect operator /(Rect r, float value)
        {
            r.X /= value;
            r.Y /= value;
            r.W /= value;
            r.H /= value;
            return r;
        }

        public static bool operator ==(Rect left, Rect right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Rect left, Rect right)
        {
            return !(left == right);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Rect r && Equals(r);
        }

        public readonly bool Equals(Rect other)
        {
            return Position == other.Position
                && Size == other.Size;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Position, Size);
        }

        public static explicit operator Vector4(Rect r)
        {
            return new Vector4(r.X, r.Y, r.W, r.H);
        }

        public static explicit operator Rect(Vector4 v)
        {
            return new Rect(v.X, v.Y, v.Z, v.W);
        }

        public override readonly string ToString()
        {
            return $"X: {X}, Y: {Y}, W: {W}, H: {H}"; ;
        }
    }
}
