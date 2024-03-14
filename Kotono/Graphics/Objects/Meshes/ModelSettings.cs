using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class ModelSettings
    {
        internal string Path { get; set; } = "";

        internal Shader Shader { get; set; } = ShaderManager.Lighting;
    }
}
