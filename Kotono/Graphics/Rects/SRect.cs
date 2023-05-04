namespace Kotono.Graphics.Rects
{
    public class SRect : IRect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float W { get; set; }

        public float H { get; set; }

        public SRect() : this(0f) { }
        
        public SRect(float n) : this(n, n, n, n) { }

        public SRect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            W = width;
            H = height;
        }

        public NRect Normalized =>
            new(
                (2 * X / KT.Width) - 1,
                1 - (2 * Y / KT.Height),
                W / KT.Width * 2,
                H / KT.Height * 2
            );

        public SRect ScreenSpace => this;

        public static bool operator ==(SRect left, SRect right)
        {
            return left.X == right.X && left.Y == right.Y && left.W == right.W && left.H == right.H;
        }

        public static bool operator !=(SRect left, SRect right)
        {
            return !(left == right);
        }

        public static SRect operator +(SRect left, SRect right)
        {
            return new(left.X + right.X, left.Y + right.Y, left.W + right.W, left.H + right.H);
        }

        public static SRect operator -(SRect left, SRect right)
        {
            return new(left.X - right.X, left.Y - right.Y, left.W - right.W, left.H - right.H);
        }

        public static SRect operator -(SRect rect)
        {
            return new(-rect.X, -rect.Y, -rect.W, -rect.H);
        }

        public static SRect operator *(SRect left, SRect right)
        {
            return new(left.X * right.X, left.Y * right.Y, left.W * right.W, left.H * right.H);
        }

        public static SRect operator *(SRect rect, float value)
        {
            return new(rect.X * value, rect.Y * value, rect.W * value, rect.H * value);
        }

        public static SRect operator /(SRect left, SRect right)
        {
            return new(left.X / right.X, left.Y / right.Y, left.W / right.W, left.H / right.H);
        }

        public static SRect operator /(SRect rect, float value)
        {
            return new(rect.X / value, rect.Y / value, rect.W / value, rect.H / value);
        }
    }
}
