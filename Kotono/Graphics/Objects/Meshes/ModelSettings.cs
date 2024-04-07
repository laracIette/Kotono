using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class ModelSettings
    {
        internal string Path { get; set; } = "";

        internal Shader Shader { get; set; } = ShaderManager.Shaders["lighting"];
    }
}
