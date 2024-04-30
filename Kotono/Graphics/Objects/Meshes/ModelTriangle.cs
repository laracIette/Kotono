using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class ModelTriangle
    {
        internal Model Model { get; }

        internal Vector[] Vertices { get; }

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

        internal ModelTriangle(Model model, Vector vertex1, Vector vertex2, Vector vertex3)
        {
            Model = model;

            Vertices = [vertex1, vertex2, vertex3];
        }
    }
}
