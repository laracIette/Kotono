using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Line(Vector start, Vector end, Transform transform, Color color)
        : Shape(
            new ShapeSettings
            {
                Transform = transform,
                Vertices = [start, end],
                Color = color
            }
        )
    {
    }
}
