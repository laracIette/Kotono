using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal interface IObject2D : IDrawable
    {
        public Rect Rect { get; set; }

        public Point Position { get; set; }

        public Point Size { get; set; }

        public int Layer { get; set; }
    }
}
