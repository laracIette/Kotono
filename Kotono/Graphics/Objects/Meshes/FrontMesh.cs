using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Managers;

namespace Kotono.Graphics.Objects.Meshes
{
    public class FrontMesh(string path, IHitbox[] hitboxes) 
        : Mesh(path, hitboxes)
    {
        protected override void Create()
        {
            ObjectManager.Create(this);
        }
    }
}
