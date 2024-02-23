using Kotono.Physics;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Meshes
{
    internal interface IMesh : IObject3D, IFizixObject
    {
        public bool IsMouseOn(out Vector intersectionLocation, out float distance);
    }
}
