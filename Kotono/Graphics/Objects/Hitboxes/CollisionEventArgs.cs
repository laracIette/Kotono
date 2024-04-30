using Kotono.Utils;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal class CollisionEventArgs(IHitbox source, IHitbox collider) : TimedEventArgs
    {
        /// <summary>
        /// The hitbox that detected the collision.
        /// </summary>
        internal IHitbox Source { get; } = source;

        /// <summary>
        /// The hitbox that collided with the source hitbox.
        /// </summary>
        internal IHitbox Collider { get; } = collider;
    }
}
