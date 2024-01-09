using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Settings;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Cube()
        : Mesh(
            new MeshSettings
            {
                Path = Path.ASSETS + @"Meshes\cube.ktf",
                Hitboxes =
                [
                    new Box(new HitboxSettings())
                ]
            }
          )
    {
    }
}
