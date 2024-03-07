using Kotono.Graphics.Objects.Hitboxes;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Meshes
{
    /// <summary>
    /// Settings class for creating a <see cref="Mesh"/>.
    /// </summary>
    internal class MeshSettings : Object3DSettings
    {
        /// <summary>
        /// The path to the 3D model of the Mesh.
        /// </summary>
        /// <remarks> 
        /// Default value : "" 
        /// </remarks>
        public string Model { get; set; } = "";

        /// <summary>
        /// The shader of the Mesh.
        /// </summary>
        /// <remarks> 
        /// Default value : "lighting" 
        /// </remarks>
        public string Shader { get; set; } = "lighting";

        /// <summary>
        /// The hitboxes of the Mesh.
        /// </summary>
        /// <remarks> 
        /// Default value : [] 
        /// </remarks>
        public List<Hitbox> Hitboxes { get; set; } = [];

        /// <summary>
        /// The textures of the Mesh.
        /// </summary>
        /// <remarks> 
        /// Default value : [] 
        /// </remarks>
        public MaterialTextureSettings[] MaterialTexturesSettings { get; set; } = [];
    }
}
