using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    public class GizmoMesh(string axis) 
        : FrontMesh(
              Path.Kotono + "Assets/Gizmo/gizmo_" + axis + ".ktf",
              []
          )
    {
    }
}
