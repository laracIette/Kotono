using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Physics;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Meshes
{
    internal interface IMesh : IObject3D, IFizixObject
    {
        internal static float IntersectionCheckFrequency => 0.1f;

        public float LastIntersectionCheckTime { get; }

        public Vector IntersectionLocation { get; }

        public float IntersectionDistance { get; }

        public Model Model { get; set; }

        public Material Material { get; set; }

        /// <summary>
        /// The hitboxes of the <see cref="IMesh"/>.
        /// </summary>
        public CustomList<Hitbox> Hitboxes { get; }
    }
}
