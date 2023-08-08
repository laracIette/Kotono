using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using CursorState = Kotono.Input.CursorState;

namespace Kotono.Graphics.Objects
{
    public static class Gizmo
    {
        private static GizmoMesh[] _meshes = new GizmoMesh[4];

        private static Mesh? _attachMesh = null;

        private static Transform _transform;

        public static Transform Transform => _transform;

        public static Vector Location
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

        public static Vector Rotation
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

        public static Vector Scale
        {
            get => _transform.Scale;
            set
            {
                _transform.Scale = value;
                foreach (var mesh in _meshes)
                {
                    mesh.Scale = value;
                }
            }
        }

        public static bool IsDraw { get; private set; } = false;

        private static int _selectedMesh = -1;

        private static TransformSpace _transformSpace = TransformSpace.World;

        public static void Init()
        {
            _meshes = new GizmoMesh[]
            {
                new GizmoMesh("x"),
                new GizmoMesh("y"),
                new GizmoMesh("z"),
                new GizmoMesh("sphere")
            };

            Hide();
        }

        public static void Update()
        {
            if (_attachMesh == null)
            {
                return;
            }

            if (Mouse.IsButtonPressed(MouseButton.Left) && (Mouse.CursorState == CursorState.Confined))
            {
                _selectedMesh = GetSelectedMesh();
            }
            else if (Mouse.IsButtonReleased(MouseButton.Left) || (Mouse.CursorState != CursorState.Confined))
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

            Scale = (Vector)(Vector.Distance(Location, CameraManager.ActiveCamera.Location) / 75);
        }

        private static Vector GetMovement()
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

        private static int GetSelectedMesh()
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

        public static void AttachTo(Mesh mesh)
        {
            _attachMesh = mesh;
        }

        public static void Detach()
        {
            _attachMesh = null;
        }

        public static void Show()
        {
            IsDraw = true;
            foreach (var mesh in _meshes)
            {
                mesh.Show();
            }
        }

        public static void Hide()
        {
            IsDraw = false;
            foreach (var mesh in _meshes)
            {
                mesh.Hide();
            }
        }
    }
}
