using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal abstract class Hitbox(HitboxSettings settings)
        : Object3D(settings),
        IHitbox
    {
        public event EventHandler<HitboxEventArgs>? EnterCollision = null;

        public event EventHandler<HitboxEventArgs>? ExitCollision = null;

        public List<IHitbox> Collisions { get; } = settings.Collisions;

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
                }
            }

            foreach (var hitbox in Colliders)
            {
                if (!colliders.Contains(hitbox))
                {
                    OnExitCollision(hitbox);
                }
            }

            Colliders = colliders;
        }

        public abstract bool CollidesWith(IHitbox hitbox);

        public bool TryGetCollider(out IHitbox? collider)
        {
            collider = Collisions.Find(CollidesWith);

            return collider != null;
        }

        public void OnEnterCollision(IHitbox hitbox)
        {
            EnterCollision?.Invoke(this, new HitboxEventArgs(this, hitbox));
        }

        public void OnExitCollision(IHitbox hitbox)
        {
            ExitCollision?.Invoke(this, new HitboxEventArgs(this, hitbox));
        }
    }
}
