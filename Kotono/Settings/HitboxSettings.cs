using Kotono.Graphics.Objects.Hitboxes;
using System.Collections.Generic;

namespace Kotono.Settings
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
        public List<Hitbox> Collisions { get; set; } = [];
    }
}
