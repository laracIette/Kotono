using Kotono.File;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class GizmoMesh(string axis)
        : FrontMesh(
            Settings.Parse<MeshSettings>(Path.ASSETS + @"Gizmo\gizmo_" + axis + ".json")
        )
    {
    }
}
