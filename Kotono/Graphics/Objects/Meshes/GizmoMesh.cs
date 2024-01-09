using Kotono.Graphics.Objects.Settings;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class GizmoMesh(string axis)
        : FrontMesh(
            new MeshSettings
            {
                Path = Path.ASSETS + @"Gizmo\gizmo_" + axis + ".ktf"
            }
        )
    {
    }
}
