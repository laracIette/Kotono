using Kotono.Graphics.Objects.Hitboxes;

namespace Kotono.Graphics.Objects.Meshes
{
    public class FrontMesh(string path, IHitbox[] hitboxes) 
        : Mesh(path, hitboxes)
    {
    }
}
