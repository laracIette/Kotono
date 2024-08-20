using Kotono.Graphics.Objects.Hitboxes;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class FrontMesh(string shader, List<Hitbox> hitboxes, string model)
        : Mesh(shader, hitboxes, model),
        IFrontMesh
    {
    }
}
