using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal abstract class Hitbox : Object3D, IHitbox
    {
        public EventHandler<CollisionEventArgs>? EnterCollision { get; set; } = null;

        public EventHandler<CollisionEventArgs>? ExitCollision { get; set; } = null;

        public List<IHitbox> Collisions { get; set; } = [];

        public List<IHitbox> Colliders { get; set; } = [];

        public bool IsColliding => TryGetCollider(out _);

        public override void Update()
        {
            var colliders = Collisions.FindAll(CollidesWith);

            foreach (var hitbox in colliders)
            {
                if (!Colliders.Contains(hitbox))
                {
                    OnEnterCollision(hitbox);
                    EnterCollision?.Invoke(this, new CollisionEventArgs(this, hitbox));
                }
            }

            foreach (var hitbox in Colliders)
            {
                if (!colliders.Contains(hitbox))
                {
                    OnExitCollision(hitbox);
                    ExitCollision?.Invoke(this, new CollisionEventArgs(this, hitbox));
                }
            }

            Colliders = colliders;
        }

        public bool TryGetCollider([NotNullWhen(true)] out IHitbox? collider)
        {
            collider = Collisions.Find(CollidesWith);
            return collider is not null;
        }

        public abstract bool CollidesWith(IHitbox hitbox);

        public virtual void OnEnterCollision(IHitbox hitbox) { }

        public virtual void OnExitCollision(IHitbox hitbox) { }
    }
}
