using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Settings
{
    internal class HitboxSettings : Object3DSettings
    {
        /// <summary>
        /// The color of the Hitbox.
        /// <para> Default value : Color.White </para>
        /// </summary>
        internal Color Color { get; set; } = Color.White;

        /// <summary>
        /// The hitboxes the Hitbox checks collisions from.
        /// <para> Default value : [] </para>
        /// </summary>
        internal List<Hitbox> Collisions { get; set; } = [];
    }
}
