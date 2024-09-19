using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal sealed class FrontMesh : Mesh, IFrontMesh
    {
        public override Shader Shader => FrontMeshShader.Instance;

        public override void UpdateShader()
        {
            if (Shader is FrontMeshShader frontMeshShader)
            {
                frontMeshShader.SetColor(Color);
                frontMeshShader.SetModel(Transform.Model);
            }
        }

        public override void Draw() => Model.Draw();

        protected override void OnLeftButtonPressed() { }
    }
}
