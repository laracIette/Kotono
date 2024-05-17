using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal interface IObject2D : IDrawable, IRect
    {
        public Rect Rect { get; set; }

        public int Layer { get; set; }
    }
}
