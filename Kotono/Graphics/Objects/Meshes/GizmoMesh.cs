using Kotono.Graphics.Shaders;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class GizmoMesh(Color color, string axis) : FrontMesh
    {
        public override Shader Shader => GizmoShader.Instance;

        public override Model Model { get; set; } = new Model(
            Path.FromAssets($@"Gizmo\gizmo_{axis}.obj"),
            GizmoShader.Instance
        );

        public override Color Color { get; set; } = color;

        public override void Update() { }
    }
}
