using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal sealed class FlatTextureMesh : Mesh
    {
        public override Shader Shader => FlatTextureShader.Instance;

        internal FlatTextureMesh()
        {
            Model = new Model(Path.FromAssets(@"Meshes\flatTextureMesh.obj"));
        }

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
