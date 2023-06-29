using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;

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
                      Scale = new Vector(.1),
                  },
                  new string[] { KT.KotonoPath + @"Assets/gizmo_diff.png" },
                  ShaderType.Gizmo,
                  color,
                  new IHitbox[] { }
              )
        {
            IsInFront = true;
        }
    }
}
