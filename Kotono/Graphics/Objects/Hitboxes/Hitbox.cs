using Kotono.File;
using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal abstract class Hitbox(HitboxSettings settings)
        : Object3D(settings),
        IHitbox
    {
        public bool IsColliding => TryGetCollider(out _);

        public List<Hitbox> Collisions { get; } = settings.Collisions;

        public List<Hitbox> Colliders => Collisions.FindAll(CollidesWith);

        public abstract bool CollidesWith(Hitbox hitbox);

        public bool TryGetCollider(out Hitbox? collider)
        {
            collider = Collisions.Find(CollidesWith);

            return collider != null;
        }
    }
}
