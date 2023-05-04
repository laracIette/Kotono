namespace Kotono.Graphics
{
    public class Rect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float W { get; set; }

        public float H { get; set; }

        public Rect() : this(0f) { }

        public Rect(float n) : this(n, n, n, n) { }

        public Rect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            W = width;
            H = height;
        }

        public Rect Normalized =>
            new(
                2 * X / KT.Width - 1,
                1 - 2 * Y / KT.Height,
                W / KT.Width * 2,
                H / KT.Height * 2
            );

        public static bool operator ==(Rect left, Rect right)
        {
            return left.X == right.X && left.Y == right.Y && left.W == right.W && left.H == right.H;
        }

        public static bool operator !=(Rect left, Rect right)
        {
            return !(left == right);
        }

        public static Rect operator +(Rect left, Rect right)
        {
            return new(left.X + right.X, left.Y + right.Y, left.W + right.W, left.H + right.H);
        }

        public static Rect operator -(Rect left, Rect right)
        {
            return new(left.X - right.X, left.Y - right.Y, left.W - right.W, left.H - right.H);
        }

        public static Rect operator -(Rect rect)
        {
            return new(-rect.X, -rect.Y, -rect.W, -rect.H);
        }

        public static Rect operator *(Rect left, Rect right)
        {
            return new(left.X * right.X, left.Y * right.Y, left.W * right.W, left.H * right.H);
        }

        public static Rect operator *(Rect rect, float value)
        {
            return new(rect.X * value, rect.Y * value, rect.W * value, rect.H * value);
        }

        public static Rect operator /(Rect left, Rect right)
        {
            return new(left.X / right.X, left.Y / right.Y, left.W / right.W, left.H / right.H);
        }

        public static Rect operator /(Rect rect, float value)
        {
            return new(rect.X / value, rect.Y / value, rect.W / value, rect.H / value);
        }
    }
}
