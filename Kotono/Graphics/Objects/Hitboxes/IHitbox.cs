using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public interface IHitbox : IObject3D
    {
        public bool CollidesWith(IHitbox h);

        public bool TryGetCollider(out IHitbox? collider);

        public bool IsColliding { get; }

        public Color Color { get; set; }

        public List<IHitbox> Collisions { get; set; }
    }
}
