namespace Kotono.Graphics.Rects
{
    public class NRect : IRect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public NRect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public NRect Normalized => this;

        public SRect ScreenSpace =>
            new(
                (X + 1) / 2 * KT.Width,
                (1 - Y) / 2 * KT.Height,
                Width * KT.Width / 2,
                Height * KT.Height / 2
            );
    }
}
