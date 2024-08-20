using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class GizmoMesh : FrontMesh
    {
        public GizmoMesh(Color color, string axis)
            : base("gizmo", [], Path.FromAssets(@"Gizmo\gizmo_" + axis + ".obj"))
        {
            Color = color;
        }

        public override void Update() { }

        protected override void OnLeftButtonPressed() { }
    }
}
