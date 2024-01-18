using Kotono.File;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Cube()
        : Mesh(
            Settings.Parse<MeshSettings>(Path.ASSETS + @"Meshes\cube.ktf")
        )
    {
    }
}
