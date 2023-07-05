using Kotono.Graphics.Objects.Meshes;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Utils
{
    public enum TransformSpace
    {
        World,
        Local
    }

    public class Gizmo
    {
        private GizmoMesh[] _meshes = new GizmoMesh[4];

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
            _meshes = new GizmoMesh[]
            {
                new GizmoMesh("x"),
                new GizmoMesh("y"),
                new GizmoMesh("z"),
                new GizmoMesh("sphere")
            };

            foreach (var mesh in _meshes)
            {
                KT.CreateMesh(mesh);
            }
        }
        
        public void Update()
        {
            if (Input.MouseState!.IsButtonPressed(MouseButton.Left) && (Input.CursorState == CursorState.Normal))
            {
                _selectedMesh = GetSelectedMesh();
            }
            else if (Input.MouseState.IsButtonReleased(MouseButton.Left) || (Input.CursorState == CursorState.Grabbed))
            {
                _selectedMesh = -1;
            }
            
            Location = _attachMesh.Location;

            switch (_transformSpace)
            {
                case TransformSpace.World:
                    Rotation = Vector.Zero;
                    break;

                case TransformSpace.Local:
                    Rotation = _attachMesh.Rotation;
                    break;

                default:
                    break;
            }

            Location += GetMovement();

            _attachMesh.Location = Location;
        }

        private Vector GetMovement()
        {
            float speed = 0.01f;

            return _selectedMesh switch
            {
                0 => Transform.Right * Mouse.Delta.X * speed,
                1 => Transform.Up * -Mouse.Delta.Y * speed,
                2 => Transform.Forward * Mouse.Delta.X * speed,
                3 => (Transform.Right * Mouse.Delta.X + Transform.Up * -Mouse.Delta.Y) * speed,
                _ => Vector.Zero
            };
        }

        private int GetSelectedMesh()
        {
            int closestMesh = -1;
            float closestDistance = float.PositiveInfinity;

            for (int i = 0; i < _meshes.Length; i++)
            {
                if (_meshes[i].IsMouseOn(out _, out float distance))
                {
                    // select the mesh that is the closest to the camera
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestMesh = i;
                    }
                }
            }

            return closestMesh;
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
