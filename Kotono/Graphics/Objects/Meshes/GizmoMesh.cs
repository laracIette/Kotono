using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class GizmoMesh(string axis)
        : FrontMesh(
            JsonParser.Parse<MeshSettings>(Path.ASSETS + @"Gizmo\gizmo_" + axis + ".json")
        )
    {
        public override void Update()
        {
        }
    }
}
