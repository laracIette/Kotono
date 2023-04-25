namespace Kotono.Graphics.Rects
{
    public class SRect : IRect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public SRect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public NRect Normalized =>
            new(
                (2 * X / KT.Width) - 1,
                1 - (2 * Y / KT.Height),
                Width / KT.Width * 2,
                Height / KT.Height * 2
            );

        public SRect ScreenSpace => this;
    }
}
