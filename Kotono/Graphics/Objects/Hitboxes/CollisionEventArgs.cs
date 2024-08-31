using Kotono.Utils;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal class CollisionEventArgs(IHitbox source, IHitbox collider) : TimedEventArgs
    {
        /// <summary>
        /// The <see cref="IHitbox"/> that detected the collision.
        /// </summary>
        internal IHitbox Source { get; } = source;

        /// <summary>
        /// The <see cref="IHitbox"/> that collided with the source <see cref="IHitbox"/>.
        /// </summary>
        internal IHitbox Collider { get; } = collider;
    }
}
