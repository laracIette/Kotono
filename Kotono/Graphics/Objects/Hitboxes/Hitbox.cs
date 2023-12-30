using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public abstract class Hitbox()
        : Object3D(),
        IHitbox
    {
        public bool IsColliding => TryGetCollider(out _);

        public Color Color { get; set; } = Color.White;

        public List<Hitbox> Collisions { get; } = [];

        public abstract bool CollidesWith(Hitbox hitbox);

        public bool TryGetCollider(out Hitbox? collider)
        {
            collider = Collisions.FirstOrDefault(CollidesWith);

            return collider != null;
        }
    }
}
