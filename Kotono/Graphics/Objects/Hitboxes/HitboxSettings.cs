using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    /// <summary>
    /// Settings class for creating an <see cref="Hitbox"/>.
    /// </summary>
    internal class HitboxSettings : Object3DSettings
    {
        /// <summary>
        /// The hitboxes the Hitbox checks collisions from.
        /// </summary>
        /// <remarks> 
        /// Default value : [] 
        /// </remarks>
        public List<IHitbox> Collisions { get; set; } = [];
    }
}
