using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Shapes
{
    internal sealed class Triangle : Shape3D
    {
        internal Triangle(Vector vertex1, Vector vertex2, Vector vertex3)
            : base([vertex1, vertex2, vertex3])
        {
            IsLoop = true;
        }
    }
}
