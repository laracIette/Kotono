using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal interface IHitbox : IDrawable
    {
        /// <summary>
        /// Occurs when the <see cref="IHitbox"/> starts colliding with an other <see cref="IHitbox"/>.
        /// </summary>
        public EventHandler<CollisionEventArgs>? EnterCollision { get; set; }

        /// <summary>
        /// Occurs when the <see cref="IHitbox"/> stops colliding with an other <see cref="IHitbox"/>.
        /// </summary>
        public EventHandler<CollisionEventArgs>? ExitCollision { get; set; }

        /// <summary>
        /// The hitboxes the <see cref="IHitbox"/> checks collision with.
        /// </summary>
        public List<IHitbox> Collisions { get; }

        /// <summary>
        /// The hitboxes the <see cref="IHitbox"/> currently collides with.
        /// </summary>
        public List<IHitbox> Colliders { get; }

        /// <summary>
        /// Wether the <see cref="IHitbox"/> is currently colliding an other <see cref="IHitbox"/>.
        /// </summary>
        public bool IsColliding { get; }

        public void OnEnterCollision(IHitbox hitbox);

        public void OnExitCollision(IHitbox hitbox);

        public bool CollidesWith(IHitbox h);

        public bool TryGetCollider([NotNullWhen(true)] out IHitbox? collider);
    }
}
