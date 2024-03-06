using Kotono.Physics;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Meshes
{
    internal interface IMesh : IObject3D, IFizixObject
    {
        internal static float IntersectionCheckFrequency => 0.1f;

        public float LastIntersectionCheckTime { get; }

        public Vector IntersectionLocation { get; }

        public float IntersectionDistance { get; }
    }
}
