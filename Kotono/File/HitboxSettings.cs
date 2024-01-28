using Kotono.Graphics.Objects.Hitboxes;
using System.Collections.Generic;

namespace Kotono.File
{
    /// <summary>
    /// Settings class for creating an <see cref="Hitbox"/>.
    /// </summary>
    internal class HitboxSettings : Object3DSettings
    {
        /// <summary>
        /// The hitboxes the Hitbox checks collisions from.
        /// <para> Default value : [] </para>
        /// </summary>
        public List<Hitbox> Collisions { get; set; } = [];
    }
}
