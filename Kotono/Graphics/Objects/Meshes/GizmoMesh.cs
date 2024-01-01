﻿using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class GizmoMesh(string axis)
        : FrontMesh(
              Path.Kotono + "Assets/Gizmo/gizmo_" + axis + ".ktf",
              []
          )
    {
    }
}
