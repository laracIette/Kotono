using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    internal interface IDrawable : IObject
    {
        public bool IsDraw { get; set; }

        public Color Color { get; set; }

        public void Draw();
    }
}
