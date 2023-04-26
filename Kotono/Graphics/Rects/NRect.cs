namespace Kotono.Graphics.Rects
{
    public class NRect : IRect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float W { get; set; }

        public float H { get; set; }

        public NRect() { }

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
    }
}
