using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Meshes;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Utils
{
    public class Gizmo
    {
        private Mesh _mesh;

        private Mesh _attachMesh;

        public Vector Location
        {
            get => _mesh.Location; 
            set => _mesh.Location = value;
        }

        public Vector Rotation
        {
            get => _mesh.Rotation; 
            set => _mesh.Rotation = value;
        }

        private Triangle _triangle;

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
                Vector.White,
                new IHitbox[] { }
            );
            KT.CreateMesh(_mesh);

            _triangle = KT.CreateTriangle(new Triangle(new Vector(1, 1, -3), new Vector(2, 3, -2), new Vector(4, 1, -3), new Transform(), Vector.Blue));
        }

        public void Update()
        {
            Location = _attachMesh.Location;
            Rotation = _attachMesh.Rotation;

            Drag();

            _attachMesh.Location = Location;
            _attachMesh.Rotation = Rotation;
        }

        private void Drag()
        {
            if (Intersection.IntersectRayTriangle(KT.ActiveCamera.Location, Input.GetMouseRay(), _triangle, out Vector intersectionPoint))
            {
                KT.Print(intersectionPoint);
            }

        }

        public void AttachTo(Mesh mesh)
        { 
            _attachMesh = mesh;
        }
    }
}
