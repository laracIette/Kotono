using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class FlatTextureMesh : Mesh
    {
        public override Shader Shader { get; set; } = ShaderManager.Shaders["flatTexture"];

        public override Model Model { get; set; } = new Model(
            Path.FromAssets(@"Meshes\flatTextureMesh.obj"),
            LightingShader.Instance
        );

        public override void Draw()
        {
            if (Shader is NewLightingShader newLightingShader)
            {
                newLightingShader.SetModel(Transform.Model);
                newLightingShader.SetBaseColor(Color);
            }

            Material.Use();

            Model.Draw();

            ITexture.Unbind();
        }
    }
}
