﻿using Kotono.File;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Utils
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        /// <summary>
        /// The position component of the Rect.
        /// </summary>
        public Point Position;

        /// <summary> 
        /// The size component of the Rect. 
        /// </summary>
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
            set => Size.X = Math.Max(0.0f, value);
        }

        /// <summary>
        /// The height component of the Rect.
        /// </summary>
        public float H
        {
            readonly get => Size.Y;
            set => Size.Y = Math.Max(0.0f, value);
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
                Size / ComponentManager.ActiveViewport.Size
            );

        /// <summary>
        /// The top left Point of the Rect.
        /// </summary>
        public readonly Point TopLeft => Position - Size / 2.0f;

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

        // TODO: add offset that moves the Rect in a direction depending on the Anchor, for example Anchor.TopLeft : X + offset, Y + offset
        /// <summary> 
        /// Creates a Rect given an Anchor.
        /// </summary>
        public static Rect FromAnchor(Rect r, Anchor a)
        {
            return a switch
            {
                Anchor.Center => r,
                Anchor.Left => new Rect(r.X + r.W / 2.0f, r.Y, r.Size),
                Anchor.Right => new Rect(r.X - r.W / 2.0f, r.Y, r.Size),
                Anchor.Top => new Rect(r.X, r.Y + r.H / 2.0f, r.Size),
                Anchor.Bottom => new Rect(r.X, r.Y - r.H / 2.0f, r.Size),
                Anchor.TopLeft => new Rect(r.X + r.W / 2.0f, r.Y + r.H / 2.0f, r.Size),
                Anchor.TopRight => new Rect(r.X - r.W / 2.0f, r.Y + r.H / 2.0f, r.Size),
                Anchor.BottomLeft => new Rect(r.X + r.W / 2.0f, r.Y - r.H / 2.0f, r.Size),
                Anchor.BottomRight => new Rect(r.X - r.W / 2.0f, r.Y - r.H / 2.0f, r.Size),
                _ => throw new Exception($"error: Rect.FromAnchor() doesn't handle \"{a}\"")
            };
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        public static bool Overlaps(Rect left, Rect right)
        {
            return (Math.Abs(left.X - right.X) < (left.W + right.W) / 2.0f)
                && (Math.Abs(left.Y - right.Y) < (left.H + right.H) / 2.0f);
        }

        /// <summary> 
        /// Checks if r is overlapping with p.
        /// </summary>
        public static bool Overlaps(Rect r, Point p)
        {
            return (Math.Abs(r.X - p.X) < r.W / 2.0f)
                && (Math.Abs(r.Y - p.Y) < r.H / 2.0f);
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        public static bool Overlaps(Image left, Image right)
        {
            return Overlaps(left.Dest, right.Dest);
        }

        public static Rect FromProperties(Properties p)
        {
            return new Rect
            {
                X = float.Parse(p["Dest.X"]),
                Y = float.Parse(p["Dest.Y"]),
                W = float.Parse(p["Dest.W"]),
                H = float.Parse(p["Dest.H"])
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

        public readonly bool Equals(Rect r)
        {
            return Position == r.Position
                && Size == r.Size;
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
