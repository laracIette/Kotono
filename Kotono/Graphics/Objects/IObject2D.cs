using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public interface IObject2D : IDrawable
    {
        public Rect Dest { get; set; }

        public int Layer { get; set; }
    }
}
