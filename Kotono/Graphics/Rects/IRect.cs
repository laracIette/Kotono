namespace Kotono.Graphics.Rects
{
    public interface IRect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float W { get; set; }

        public float H { get; set; }

        public NRect Normalized { get; }

        public SRect ScreenSpace { get; }
    }
}
