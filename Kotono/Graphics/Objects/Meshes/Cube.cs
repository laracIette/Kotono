using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    public class Cube()
        : Mesh(
              Path.Assets + @"Meshes\cube.ktf",
              [
                  new Box()
              ]
          )
    {
    }
}
