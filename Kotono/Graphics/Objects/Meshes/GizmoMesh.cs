using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    public class GizmoMesh : FrontMesh
    {
        public GizmoMesh(string axis)
            : base(
                  Path.Kotono + "Assets/Gizmo/gizmo_" + axis + ".ktf",
                  new IHitbox[] { }
              )
        {
        }
    }
}
