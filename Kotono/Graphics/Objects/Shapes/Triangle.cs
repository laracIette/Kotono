using Kotono.Utils;

namespace Kotono.Graphics.Objects.Shapes
{
    public class Triangle(Vector vertex1, Vector vertex2, Vector vertex3, Transform transform, Color color) 
        : Shape([vertex1, vertex2, vertex3], transform, color)
    {
    }
}
