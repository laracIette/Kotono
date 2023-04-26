namespace Kotono.Graphics.Rects
{
    public class SRect : IRect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float W { get; set; }

        public float H { get; set; }

        public SRect() { }

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
    }
}
