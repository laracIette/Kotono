using Assimp;

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

        public static Rect operator -(Rect rect)
        {
            rect.X = -rect.X;
            rect.Y = -rect.Y;
            rect.W = -rect.W;
            rect.H = -rect.H;
            return rect;
        }

        public static Rect operator *(Rect left, Rect right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.W *= right.W;
            left.H *= right.H;
            return left;
        }

        public static Rect operator *(Rect rect, float value)
        {
            rect.X *= value;
            rect.Y *= value;
            rect.W *= value;
            rect.H *= value;
            return rect;
        }

        public static Rect operator /(Rect left, Rect right)
        {
            left.X /= right.X;
            left.Y /= right.Y;
            left.W /= right.W;
            left.H /= right.H;
            return left;
        }

        public static Rect operator /(Rect rect, float value)
        {
            rect.X /= value;
            rect.Y /= value;
            rect.W /= value;
            rect.H /= value;
            return rect;
        }
    }
}
