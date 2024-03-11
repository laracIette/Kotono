using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Cube()
        : Mesh(
            JsonParser.Parse<MeshSettings>(Path.ASSETS + @"Meshes\cube.json")
        )
    {
    }
}
