using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Cube()
        : Mesh(
              Path.Assets + @"Meshes\cube.ktf",
              [
                  new Box()
              ]
          )
    {
    }
}
