using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public interface IObject2D
    {
        public Rect Dest { get; set; }

        public Point Position { get; set; }

        public Point Size { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float W { get; set; }

        public float H { get; set; }

        public int Layer { get; set; }
    }
}
