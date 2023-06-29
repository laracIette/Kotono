using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    public class GizmoMesh : Mesh
    {
        public GizmoMesh(string axis, Color color)
            : base(
                  KT.KotonoPath + @"Assets/gizmo_" + axis + ".obj",
                  new Transform
                  {
                      Location = Vector.Zero,
                      Rotation = Vector.Zero,
                      Scale = new Vector(.2),
                  },
                  KT.KotonoPath + @"Assets/gizmo_diff.png",
                  KT.KotonoPath + @"Assets/gizmo_spec.png",
                  ShaderType.Lighting,
                  color,
                  new IHitbox[] { }
              )
        {
            IsInFront = true;
        }
    }
}
