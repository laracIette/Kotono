using Kotono.Utils;

namespace Kotono.Graphics.Objects.Shapes
{
    internal interface IShape : IDrawable
    {
        public Vector[] Vertices { get; }
    }
}
