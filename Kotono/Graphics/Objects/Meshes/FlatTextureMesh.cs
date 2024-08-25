using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class FlatTextureMesh : Mesh
    {
        public override Shader Shader { get; set; } = ShaderManager.Shaders["flatTexture"];

        public override Model Model { get; set; } = Model.Load(new ModelSettings
        {
            Path = Path.FromAssets(@"Meshes\flatTextureMesh.obj"),
            Shader = ShaderManager.Shaders["lighting"]
        });

        public override void Draw()
        {
            if (Shader is NewLightingShader newLightingShader)
            {
                newLightingShader.SetModel(Transform.Model);
                newLightingShader.SetBaseColor(Color);
            }

            Material.Use();

            Model.Draw();

            Texture.Bind(0);
        }
    }
}
