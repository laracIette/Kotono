using Kotono.Graphics;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Meshes;

namespace Kotono.Utils
{
    public class Gizmo
    {
        private Mesh _mesh;

        private Mesh _attachMesh;

        public Gizmo() { }

        public void Init()
        {
            _mesh = new Mesh(
                KT.KotonoPath + @"Assets/gizmo.obj",
                new Transform
                {
                    Location = Vector.Zero,
                    Rotation = Vector.Zero,
                    Scale = new Vector(.2),
                },
                KT.KotonoPath + @"Assets/gizmo_diff.png",
                KT.KotonoPath + @"Assets/gizmo_spec.png",
                ShaderType.Lighting,
                Vector.Unit,
                new IHitbox[] { }
            );
            KT.CreateMesh(_mesh);
        }

        public void Update()
        {
            _mesh.Location = _attachMesh.Location;
            _mesh.Rotation = _attachMesh.Rotation;
        }

        public void AttachTo(Mesh mesh)
        { 
            _attachMesh = mesh;
        }
    }
}
