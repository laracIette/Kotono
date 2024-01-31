using Kotono.File;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class FrontMesh(MeshSettings settings)
        : Mesh(settings), 
        IFrontMesh
    {
    }
}
