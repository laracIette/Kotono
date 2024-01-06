using Kotono.Graphics.Objects.Settings;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Triangle(Vector vertex1, Vector vertex2, Vector vertex3, Transform transform, Color color)
        : Shape(
            new ShapeSettings
            {
                Location = transform.Location,
                Rotation = transform.Rotation,
                Scale = transform.Scale,
                Vertices = [vertex1, vertex2, vertex3], 
                Color = color
            }
        )
    {
    }
}
