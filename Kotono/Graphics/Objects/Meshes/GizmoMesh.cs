using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal sealed class GizmoMesh : FrontMesh
    {
        public override Shader Shader => GizmoShader.Instance;

        public override void UpdateShader()
        {
            if (Shader is GizmoShader gizmoShader)
            {
                gizmoShader.SetColor(Color);
                gizmoShader.SetModel(Transform.Model);
            }
        }

        public override void Draw() => Model.Draw();
    }
}
