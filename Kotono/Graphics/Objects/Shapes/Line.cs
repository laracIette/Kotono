using Kotono.Settings;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Line(Vector start, Vector end, Transform transform, Color color)
        : Shape(
            new ShapeSettings
            {
                Location = transform.Location,
                Rotation = transform.Rotation,
                Scale = transform.Scale,
                Vertices = [start, end],
                Color = color
            }
        )
    {
    }
}
