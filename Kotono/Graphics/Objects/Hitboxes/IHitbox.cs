using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public interface IHitbox
    {
        public bool IsColliding { get; }

        public Color Color { get; set; }

        public List<Hitbox> Collisions { get; }

        public bool CollidesWith(Hitbox h);

        public bool TryGetCollider(out Hitbox? collider);
    }
}
