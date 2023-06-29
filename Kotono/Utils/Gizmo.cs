using Kotono.Graphics.Objects.Meshes;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Numerics;

namespace Kotono.Utils
{
    public enum TransformSpace
    {
        World,
        Local
    }

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

        public bool IsDraw = true;

        private int _selectedMesh = -1;

        private TransformSpace _transformSpace = TransformSpace.World;

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
        }

        public void Update()
        {
            if (Input.MouseState!.IsButtonPressed(MouseButton.Left))
            {
                _selectedMesh = GetSelectedMesh();
            }
            else if (Input.MouseState.IsButtonReleased(MouseButton.Left))
            {
                _selectedMesh = -1;
            }

            Location = _attachMesh.Location;

            switch (_transformSpace)
            {
                case TransformSpace.World:
                    MoveWorld();
                    break;

                case TransformSpace.Local:
                    MoveLocal();
                    break;

                default:
                    break;
            }

            _attachMesh.Location = Location;
        }

        private void MoveWorld()
        {
            Rotation = Vector.Zero;

            Location += GetMovement();
        }

        private void MoveLocal()
        {
            Rotation = _attachMesh.Rotation;

            Location += GetMovement();
        }

        private Vector GetMovement()
        {
            return _selectedMesh switch
            {
                0 => Transform.Right * Input.MouseState!.Delta.X * .01f,
                1 => Transform.Up * Input.MouseState!.Delta.X * .01f,
                2 => Transform.Forward * Input.MouseState!.Delta.X * .01f,
                _ => Vector.Zero
            };
        }

        private int GetSelectedMesh()
        {
            if (Input.CursorState == CursorState.Grabbed)
            {
                return -1;
            }

            for (int i = 0; i < _meshes.Length; i++)
            {
                foreach (var triangle in _meshes[i].Triangles)
                {
                    triangle.Transform = _meshes[i].Transform;
                    if (Intersection.IntersectRayTriangle(KT.ActiveCamera.Location, Input.GetMouseRay(), triangle, out _))
                    {
                        return i;
                    }
                }
            }

            return -1;
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
