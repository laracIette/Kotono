using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Physics;
using Kotono.Utils.Coordinates;
using System.Collections.Generic;

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

        public List<Hitbox> Hitboxes { get; set; }
    }
}
