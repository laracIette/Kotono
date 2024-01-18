using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.File
{
    internal class HitboxSettings : Object3DSettings
    {
        /// <summary>
        /// The hitboxes the Hitbox checks collisions from.
        /// <para> Default value : [] </para>
        /// </summary>
        [Parsable]
        public List<Hitbox> Collisions { get; set; } = [];
    }
}
