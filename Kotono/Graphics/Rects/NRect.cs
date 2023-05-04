namespace Kotono.Graphics.Rects
{
    public class NRect : IRect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float W { get; set; }

        public float H { get; set; }

        public NRect() : this(0f) { }
        
        public NRect(float n) : this(n, n, n, n) { }

        public NRect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            W = width;
            H = height;
        }

        public NRect Normalized => this;

        public SRect ScreenSpace =>
            new(
                (X + 1) / 2 * KT.Width,
                (1 - Y) / 2 * KT.Height,
                W * KT.Width / 2,
                H * KT.Height / 2
            );

        public static bool operator ==(NRect left, NRect right)
        {
            return left.X == right.X && left.Y == right.Y && left.W == right.W && left.H == right.H;
        }

        public static bool operator !=(NRect left, NRect right)
        {
            return !(left == right);
        }
    }
}
