using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Managers;

namespace Kotono.Graphics.Objects.Meshes
{
    public class FrontMesh : Mesh
    {
        public FrontMesh(string path, IHitbox[] hitboxes)
            : base(path, hitboxes)
        { }

        protected override void Create()
        {
            ObjectManager.CreateFrontMesh(this);
        }
    }
}
