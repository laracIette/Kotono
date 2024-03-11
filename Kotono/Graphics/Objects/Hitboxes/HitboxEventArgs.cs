using System;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal class HitboxEventArgs(IHitbox source, IHitbox collider) 
        : EventArgs()
    {
        internal IHitbox Source { get; set; } = source;

        internal IHitbox Collider { get; set; } = collider;
    }
}
