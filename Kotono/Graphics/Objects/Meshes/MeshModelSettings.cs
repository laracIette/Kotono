using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class MeshModelSettings
    {
        internal string Path { get; set; } = "";

        internal Shader Shader { get; set; } = ShaderManager.Lighting;
    }
}
