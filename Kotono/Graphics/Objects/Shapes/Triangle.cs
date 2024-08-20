using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Triangle(Vector vertex1, Vector vertex2, Vector vertex3) 
        : Shape([vertex1, vertex2, vertex3], true)
    {
    }
}
