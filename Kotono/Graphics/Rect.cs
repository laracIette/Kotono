using Assimp;

namespace Kotono.Graphics
{
    public class Rect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float W { get; set; }

        public float H { get; set; }

        public Rect() : this(0f, 0f, 0f, 0f) { }

        public Rect(float x = 0, float y = 0, float w = 0, float h = 0) 
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public Rect Normalized =>
            new(
                2 * X / KT.CurrentViewportWidth - 1,
                1 - 2 * Y / KT.CurrentViewportHeight,
                W / KT.CurrentViewportWidth * 2,
                H / KT.CurrentViewportHeight * 2
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
            return new Rect(left.X + right.X, left.Y + right.Y, left.W + right.W, left.H + right.H);
        }

        public static Rect operator -(Rect left, Rect right)
        {
            return new Rect(left.X - right.X, left.Y - right.Y, left.W - right.W, left.H - right.H);
        }

        public static Rect operator -(Rect rect)
        {
            return new Rect(-rect.X, -rect.Y, -rect.W, -rect.H);
        }

        public static Rect operator *(Rect left, Rect right)
        {
            return new Rect(left.X * right.X, left.Y * right.Y, left.W * right.W, left.H * right.H);
        }

        public static Rect operator *(Rect rect, float value)
        {
            return new Rect(rect.X * value, rect.Y * value, rect.W * value, rect.H * value);
        }

        public static Rect operator /(Rect left, Rect right)
        {
            return new Rect(left.X / right.X, left.Y / right.Y, left.W / right.W, left.H / right.H);
        }

        public static Rect operator /(Rect rect, float value)
        {
            return new Rect(rect.X / value, rect.Y / value, rect.W / value, rect.H / value);
        }
    }
}
