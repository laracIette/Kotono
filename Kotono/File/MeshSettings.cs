using Kotono.Graphics;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Shaders;
using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.File
{
    internal class MeshSettings : Object3DSettings
    {
        /// <summary>
        /// The path to the 3D model of the Mesh.
        /// <para> Default value : "" </para>
        /// </summary>
        [Parsable]
        public string Model { get; set; } = "";

        /// <summary>
        /// The shader of the Mesh.
        /// <para> Default value : ShaderManager.Lighting </para>
        /// </summary>
        [Parsable]
        public Shader Shader { get; set; } = ShaderManager.Lighting;

        /// <summary>
        /// The hitboxes of the Mesh.
        /// <para> Default value : [] </para>
        /// </summary>
        [Parsable]
        public List<Hitbox> Hitboxes { get; set; } = [];

        /// <summary>
        /// The textures of the Mesh.
        /// <para> Default value : [] </para>
        /// </summary>
        [Parsable]
        public List<Texture> Textures { get; set; } = [];
    }
}
