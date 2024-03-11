using Kotono.Utils;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal interface IHitbox : IDrawable
    {
        public event EventHandler<CollisionEventArgs>? EnterCollision;

        public event EventHandler<CollisionEventArgs>? ExitCollision;

        /// <summary>
        /// The hitboxes the IHitbox checks collision with.
        /// </summary>
        public List<IHitbox> Collisions { get; }

        /// <summary>
        /// The hitboxes the IHitbox currently collides with.
        /// </summary>
        public List<IHitbox> Colliders { get; }

        public bool IsColliding { get; }

        public void OnEnterCollision(IHitbox hitbox);

        public void OnExitCollision(IHitbox hitbox);

        public bool CollidesWith(IHitbox h);

        public bool TryGetCollider(out IHitbox? collider);
    }
}
