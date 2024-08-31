using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Cube : Mesh
    {
        public override Material Material { get; set; } = new()
        {
            Albedo = new MaterialTexture(Path.FromAssets(@"Meshes\cube_diff.png"))
        };

        public override Model Model { get; set; } = new Model(
            Path.FromAssets(@"Meshes\cube.obj"),
            NewLightingShader.Instance
        );
    }
}
