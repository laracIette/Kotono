using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Kotono.Utils.Coordinates
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect : IEquatable<Rect>
    {
        private record class Transformation(Rect TransformBase, float EndTime);

        /// <summary>
        /// The position component of the Rect.
        /// </summary>
        public Point Position { get; set; }

        /// <summary> 
        /// The size component of the Rect. 
        /// </summary>
        public Point Size;

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
            Matrix4.CreateScale(NDC.Size.X, NDC.Size.Y, 1.0f)
            * Matrix4.CreateTranslation(NDC.Position.X, NDC.Position.Y, 0.0f);

        /// <summary>
        /// The center Point of the Rect.
        /// </summary>
        public readonly Point Center => Position;

        /// <summary>
        /// The left Point of the Rect.
        /// </summary>
        public readonly Point Left => new Point(Position.X - Size.X / 2.0f, Position.Y);

        /// <summary>
        /// The right Point of the Rect.
        /// </summary>
        public readonly Point Right => new Point(Position.X + Size.X / 2.0f, Position.Y);

        /// <summary>
        /// The top Point of the Rect.
        /// </summary>
        public readonly Point Top => new Point(Position.X, Position.Y + Size.Y / 2.0f);

        /// <summary>
        /// The bottom Point of the Rect.
        /// </summary>
        public readonly Point Bottom => new Point(Position.X, Position.Y - Size.Y / 2.0f);

        /// <summary>
        /// The top left Point of the Rect.
        /// </summary>
        public readonly Point TopLeft => new Point(Position.X - Size.X / 2.0f, Position.Y + Size.Y / 2.0f);

        /// <summary>
        /// The top right Point of the Rect.
        /// </summary>
        public readonly Point TopRight => new Point(Position.X + Size.X / 2.0f, Position.Y + Size.Y / 2.0f);

        /// <summary>
        /// The bottom left Point of the Rect.
        /// </summary>
        public readonly Point BottomLeft => new Point(Position.X - Size.X / 2.0f, Position.Y - Size.Y / 2.0f);

        /// <summary>
        /// The bottom right Point of the Rect.
        /// </summary>
        public readonly Point BottomRight => new Point(Position.X + Size.X / 2.0f, Position.Y - Size.Y / 2.0f);

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


        public Rect(float x = 0.0f, float y = 0.0f, float w = 0.0f, float h = 0.0f)
        {
            Position = new Point(x, y);
            Size = new Point(w, h);
        }

        public Rect() : this(0.0f, 0.0f, 0.0f, 0.0f) { }

        public Rect(Rect r) : this(r.Position.X, r.Position.Y, r.Size.X, r.Size.Y) { }

        public Rect(float f) : this(f, f, f, f) { }

        public Rect(Point position, Point size) : this(position.X, position.Y, size.X, size.Y) { }

        public Rect(Point position, float w, float h) : this(position.X, position.Y, w, h) { }

        public Rect(float x, float y, Point size) : this(x, y, size.X, size.Y) { }

        public static Rect FromAnchor(Rect r, Anchor a, Point offset)
        {
            r.Position += a switch
            {
                Anchor.Center => offset,
                Anchor.Left => new Point(r.Size.X / 2.0f + offset.X, offset.Y),
                Anchor.Right => new Point(-r.Size.X / 2.0f - offset.X, offset.Y),
                Anchor.Top => new Point(offset.X, r.Size.Y / 2.0f + offset.Y),
                Anchor.Bottom => new Point(offset.X, -r.Size.Y / 2.0f - offset.Y),
                Anchor.TopLeft => new Point(r.Size.X / 2.0f + offset.X, r.Size.Y / 2.0f + offset.Y),
                Anchor.TopRight => new Point(-r.Size.X / 2.0f - offset.X, r.Size.Y / 2.0f + offset.Y),
                Anchor.BottomLeft => new Point(r.Size.X / 2.0f + offset.X, -r.Size.Y / 2.0f - offset.Y),
                Anchor.BottomRight => new Point(-r.Size.X / 2.0f - offset.X, -r.Size.Y / 2.0f - offset.Y),
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
                float y = a switch
                {
                    Anchor.Center or Anchor.Left or Anchor.Right => r.Position.Y - r.Size.Y / 2.0f * (n - 1) + r.Size.Y * i,
                    Anchor.Top or Anchor.TopLeft or Anchor.TopRight => throw new NotImplementedException(),
                    Anchor.Bottom or Anchor.BottomLeft or Anchor.BottomRight => throw new NotImplementedException(),
                    _ => throw new SwitchException(typeof(Anchor), a)
                };

                result[i].Position = new Point(result[i].Position.X, y);
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
            return Math.Abs(left.Position.X - right.Position.X) < (left.Size.X + right.Size.X) / 2.0f
                && Math.Abs(left.Position.Y - right.Position.Y) < (left.Size.Y + right.Size.Y) / 2.0f;
        }

        /// <summary> 
        /// Checks if r is overlapping with p.
        /// </summary>
        public static bool Overlaps(Rect r, Point p)
        {
            return Math.Abs(r.Position.X - p.X) < r.Size.X / 2.0f
                && Math.Abs(r.Position.Y - p.Y) < r.Size.Y / 2.0f;
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        internal static bool Overlaps(Image left, Image right)
        {
            return Overlaps(left.Rect, right.Rect);
        }

        public static Rect Parse(string[] values)
        {
            return new Rect
            {
                Position = (float.Parse(values[0]), float.Parse(values[1])),
                Size = (float.Parse(values[2]), float.Parse(values[3]))
            };
        }

        public static Rect operator +(Rect left, Rect right)
        {
            left.Position += right.Position;
            left.Size += right.Size;
            return left;
        }

        public static Rect operator -(Rect left, Rect right)
        {
            left.Position -= right.Position;
            left.Size -= right.Size;
            return left;
        }

        public static Rect operator -(Rect r)
        {
            r.Position = -r.Position;
            r.Size = -r.Size;
            return r;
        }

        public static Rect operator *(Rect left, Rect right)
        {
            left.Position *= right.Position;
            left.Size *= right.Size;
            return left;
        }

        public static Rect operator *(Rect r, float value)
        {
            r.Position *= value;
            r.Size *= value;
            return r;
        }

        public static Rect operator /(Rect left, Rect right)
        {
            left.Position /= right.Position;
            left.Size /= right.Size;
            return left;
        }

        public static Rect operator /(Rect r, float value)
        {
            r.Position /= value;
            r.Size /= value;
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
            return new Vector4(r.Position.X, r.Position.Y, r.Size.X, r.Size.Y);
        }

        public static explicit operator Rect(Vector4 v)
        {
            return new Rect(v.X, v.Y, v.Z, v.W);
        }

        public override readonly string ToString()
        {
            return $"X: {Position.X}, Y: {Position.Y}, W: {Size.X}, H: {Size.Y}"; ;
        }
    }
}
