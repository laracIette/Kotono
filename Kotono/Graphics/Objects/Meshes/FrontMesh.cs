using Kotono.Graphics.Objects.Hitboxes;

namespace Kotono.Graphics.Objects.Meshes
{
    public class FrontMesh : Mesh
    {
        public FrontMesh(string path, IHitbox[] hitboxes)
            : base(path, hitboxes)
        { }

        protected override void Create()
        {
            KT.CreateFrontMesh(this);
        }
    }
}
