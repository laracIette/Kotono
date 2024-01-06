using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal interface IHitbox
    {
        public bool IsColliding { get; }

        public Color Color { get; set; }

        public List<Hitbox> Collisions { get; }

        public List<Hitbox> Colliders { get; }

        public bool CollidesWith(Hitbox h);

        public bool TryGetCollider(out Hitbox? collider);
    }
}
