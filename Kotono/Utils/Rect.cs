using Assimp;
using Kotono.Graphics.Objects;
using Kotono.Input;
using OpenTK.Mathematics;
using System;

namespace Kotono.Utils
{
    public struct Rect
    {
        public Point Position;

        public Point Size;

        public float X
        { 
            readonly get => Position.X;
            set => Position.X = value;
        }

        public float Y
        {
            readonly get => Position.Y;
            set => Position.Y = value;
        }

        public float W
        {
            readonly get => Size.X;
            set => Size.X = Math.Clamp(value, 0, float.PositiveInfinity);
        }

        public float H
        {
            readonly get => Size.Y;
            set => Size.Y = Math.Clamp(value, 0, float.PositiveInfinity);
        }

        public readonly Rect Normalized =>
            new Rect(
                Position.Normalized,
                Size.Normalized
            );

        public readonly Rect TopLeft =>
            new Rect(
                Position - Size / 2,
                Size
            );

        public static Rect Zero => new Rect(0, 0, 0, 0);
        
        public static Rect Unit => new Rect(1, 1, 1, 1);
       
        public static Rect UnitX => new Rect(1, 0, 0, 0);

        public static Rect UnitY => new Rect(0, 1, 0, 0);
        
        public static Rect UnitW => new Rect(0, 0, 1, 0);
        
        public static Rect UnitH => new Rect(0, 0, 0, 1);

        public readonly Rect WorldSpace =>
            new Rect(
                2 * X / KT.ActiveViewport.W - 1,
                1 - 2 * Y / KT.ActiveViewport.H,
                W / KT.ActiveViewport.W,
                H / KT.ActiveViewport.H
            );


        public Rect()
        {
            X = 0;
            Y = 0;
            W = 0;
            H = 0;
        }

        public Rect(Rect r)
        {
            X = r.X;
            Y = r.Y;
            W = r.W;
            H = r.H;
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

        public Rect(float x = 0, float y = 0, float w = 0, float h = 0)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public Rect(double x = 0, double y = 0, double w = 0, double h = 0)
        {
            X = (float)x;
            Y = (float)y;
            W = (float)w;
            H = (float)h;
        }

        /// <summary> Creates a Rect given an Anchor </summary>
        public static Rect FromAnchor(Rect r, Anchor a)
        {
            return a switch
            {
                Anchor.Center => r,
                Anchor.Left => new Rect(r.X + r.W / 2, r.Y, r.Size),
                Anchor.Right => new Rect(r.X - r.W / 2, r.Y, r.Size),
                Anchor.Top => new Rect(r.X, r.Y + r.H / 2, r.Size),
                Anchor.Bottom => new Rect(r.X, r.Y - r.H / 2, r.Size),
                Anchor.TopLeft => new Rect(r.X + r.W / 2, r.Y + r.H / 2, r.Size),
                Anchor.TopRight => new Rect(r.X - r.W / 2, r.Y + r.H / 2, r.Size),
                Anchor.BottomLeft => new Rect(r.X + r.W / 2, r.Y - r.H / 2, r.Size),
                Anchor.BottomRight => new Rect(r.X - r.W / 2, r.Y - r.H / 2, r.Size),
                _ => throw new Exception($"error: Rect.FromAnchor() doesn't handle \"{a}\"")
            };
        }

        /// <summary> Checks if left is overlapping with right </summary>
        public static bool Overlaps(Rect left, Rect right)
        {
            return (Math.Abs(left.X - right.X) < (left.W + right.W) / 2) && (Math.Abs(left.Y - right.Y) < (left.H + right.H) / 2);
        }

        /// <summary> Checks if r is overlapping with p </summary>
        public static bool Overlaps(Rect r, Point p)
        {
            return (Math.Abs(r.X - p.X) < r.W / 2) && (Math.Abs(r.Y - p.Y) < r.H / 2);
        }

        /// <summary> Checks if left is overlapping with right </summary>
        public static bool Overlaps(Image left, Image right)
        {
            return Overlaps(left.Dest, right.Dest);
        }

        public static Rect Add(Rect r, float x = 0, float y = 0, float w = 0, float h = 0)
        {
            r.X += x;
            r.Y += y;
            r.W += w;
            r.H += h;
            return r;
        }

        public static Rect Substract(Rect r, float x = 0, float y = 0, float w = 0, float h = 0)
        {
            r.X -= x;
            r.Y -= y;
            r.W -= w;
            r.H -= h;
            return r;
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
            return left.X == right.X && left.Y == right.Y && left.W == right.W && left.H == right.H;
        }

        public static bool operator !=(Rect left, Rect right)
        {
            return !(left == right);
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
