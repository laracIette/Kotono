﻿namespace Kotono.Utils
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
            set => Size.X = value;
        }

        public float H
        {
            readonly get => Size.Y;
            set => Size.Y = value;
        }

        public readonly Rect Normalized =>
            new Rect(
                Position.Normalized,
                Size.Normalized
            );

        public static Rect Zero => new Rect(0, 0, 0, 0);

        public readonly Rect WorldSpace =>
            new Rect(
                2 * X / KT.ActiveViewport.W - 1,
                1 - 2 * Y / KT.ActiveViewport.H,
                W / KT.ActiveViewport.W * 2,
                H / KT.ActiveViewport.H * 2
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
            Position = r.Position; 
            Size = r.Size;
        }

        public Rect(Point position, Point size)
        {
            Position = position;
            Size = size;
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

        public override readonly string ToString()
        {
            return $"X: {X}, Y: {Y}, W: {W}, H: {H}"; ;
        }
    }
}