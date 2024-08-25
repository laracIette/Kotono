using Kotono.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Cube : Mesh
    {
        public override Material Material { get; set; } = new()
        {
            Albedo = new MaterialTexture(Path.FromAssets(@"Meshes\cube_diff.png"), TextureUnit.Texture0)
        };

        public override Model Model { get; set; } = Model.Load(new ModelSettings
        {
            Path = Path.FromAssets(@"Meshes\cube.obj"),
            Shader = ShaderManager.Shaders["lighting"]
        });
    }
}
