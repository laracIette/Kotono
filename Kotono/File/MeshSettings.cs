﻿using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Meshes;
using System.Collections.Generic;

namespace Kotono.File
{
    /// <summary>
    /// Settings class for creating a <see cref="Mesh"/>.
    /// </summary>
    internal class MeshSettings : Object3DSettings
    {
        /// <summary>
        /// The path to the 3D model of the Mesh.
        /// <para> Default value : "" </para>
        /// </summary>
        public string Model { get; set; } = "";

        /// <summary>
        /// The shader of the Mesh.
        /// <para> Default value : "lighting" </para>
        /// </summary>
        public string Shader { get; set; } = "lighting";

        /// <summary>
        /// The hitboxes of the Mesh.
        /// <para> Default value : [] </para>
        /// </summary>
        public List<Hitbox> Hitboxes { get; set; } = [];

        /// <summary>
        /// The textures of the Mesh.
        /// <para> Default value : [] </para>
        /// </summary>
        public string[] Textures { get; set; } = [];
    }
}