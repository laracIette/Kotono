using Kotono.Graphics.Objects.Hitboxes;

namespace Kotono.Graphics.Objects.Meshes
{
    public class GizmoMesh : Mesh
    {
        public GizmoMesh(string axis)
            : base(
                  KT.KotonoPath + "Assets/Gizmo/gizmo_" + axis + ".ktf",
                  new IHitbox[] { }
              )
        {
            IsInFront = true;
        }
    }
}
