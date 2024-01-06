using Kotono.Graphics.Objects.Hitboxes;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Settings
{
    internal class MeshSettings : Object3DSettings
    {
        /// <summary>
        /// The path to the Mesh's file.
        /// <para> Default value : "" </para>
        /// </summary>
        internal string Path { get; set; } = "";

        /// <summary>
        /// The hitboxes of the Mesh.
        /// <para> Default value : [] </para>
        /// </summary>
        internal List<Hitbox> Hitboxes { get; set; } = [];
    }
}
