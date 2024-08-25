using Kotono.Graphics.Shaders;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class GizmoMesh(Color color, string axis) : FrontMesh
    {
        public override Shader Shader { get; set; } = ShaderManager.Shaders["gizmo"];

        public override Model Model { get; set; } = Model.Load(new ModelSettings
        {
            Path = Path.FromAssets(@"Gizmo\gizmo_" + axis + ".obj"),
            Shader = ShaderManager.Shaders["gizmo"]
        });

        public override Color Color { get; set; } = color;

        public override void Update() { }

        protected override void OnLeftButtonPressed() { }
    }
}
