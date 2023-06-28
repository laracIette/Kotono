using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Meshes;
using Newtonsoft.Json.Linq;
using OpenTK.Windowing.Common;

namespace Kotono.Utils
{
    public class Gizmo
    {
        private Mesh[] _meshes = new Mesh[4];

        private Mesh _attachMesh;

        private Transform _transform;

        public Transform Transform => _transform;

        public Vector Location
        {
            get => _transform.Location;
            set
            {
                _transform.Location = value;
                foreach (var mesh in _meshes)
                {
                    mesh.Location = value;
                }
            }
        }

        public Vector Rotation
        {
            get => _transform.Rotation;
            set
            {
                _transform.Rotation = value;
                foreach (var mesh in _meshes)
                {
                    mesh.Rotation = value;
                }
            }
        }

        private Triangle _triangle;

        public bool IsDraw = true;

        public Gizmo() { }

        public void Init()
        {
            _meshes = new Mesh[]
            {
                new GizmoMesh("x", Vector.Red),
                new GizmoMesh("y", Vector.Green),
                new GizmoMesh("z", Vector.Blue),
                new GizmoMesh("sphere", Vector.White)
            };

            foreach (var mesh in _meshes)
            {
                KT.CreateMesh(mesh);
            }

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
            if (Input.CursorState == CursorState.Grabbed)
            {
                return;
            }

            if (Intersection.IntersectRayTriangle(KT.ActiveCamera.Location, Input.GetMouseRay(), _triangle, out Vector intersectionPoint))
            {
                KT.Print(intersectionPoint);
            }

        }

        public void AttachTo(Mesh mesh)
        {
            _attachMesh = mesh;
        }

        public void Show()
        {
            IsDraw = true;
            foreach (var mesh in _meshes)
            {
                mesh.Show();
            }
        }

        public void Hide()
        {
            IsDraw = false;
            foreach (var mesh in _meshes)
            {
                mesh.Hide();
            }
        }
    }
}
