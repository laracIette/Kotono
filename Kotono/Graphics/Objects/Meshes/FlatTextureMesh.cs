using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class FlatTextureMesh : Mesh
    {
        public override Shader Shader => FlatTextureShader.Instance;

        public override Model Model { get; set; } = new Model(
            Path.FromAssets(@"Meshes\flatTextureMesh.obj"),
            FlatTextureShader.Instance
        );

        public override void UpdateShader()
        {
            if (Shader is FlatTextureShader flatTextureShader)
            {
                flatTextureShader.SetModel(Transform.Model);
                flatTextureShader.SetColor(Color);
            }
        }
    }
}
