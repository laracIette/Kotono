namespace Kotono.Graphics.Rects
{
    public interface IRect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public NRect Normalized { get; }

        public SRect ScreenSpace { get; }
    }
}
