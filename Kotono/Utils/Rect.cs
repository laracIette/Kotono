using OpenTK.Mathematics;

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
