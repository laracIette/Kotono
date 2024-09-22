using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;

namespace Kotono.Graphics.Objects.Meshes
{
    internal sealed class ModelTriangle
    {
        internal Vector[] Vertices { get; }

        internal Vector Center => Vector.Avg(Vertices);

        internal Vector this[int index]
        {
            get => index switch
            {
                0 => Vertices[0],
                1 => Vertices[1],
                2 => Vertices[2],
                _ => throw new SwitchException(typeof(int), index)
            };
        }

        internal ModelTriangle(Vector vertex1, Vector vertex2, Vector vertex3)
            => Vertices = [vertex1, vertex2, vertex3];
    }
}
