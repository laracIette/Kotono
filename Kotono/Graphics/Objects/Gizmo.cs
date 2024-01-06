using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using CursorState = Kotono.Input.CursorState;

namespace Kotono.Graphics.Objects
{
    internal static class Gizmo
    {
        private static readonly GizmoMesh[] _meshes =
        [
            new GizmoMesh("x"),
            new GizmoMesh("y"),
            new GizmoMesh("z"),
            new GizmoMesh("sphere")
        ];

        private static Mesh? _attachMesh = null;

        private static Transform _transform;

        internal static Transform Transform => _transform;

        internal static Vector Location
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

        internal static Vector Rotation
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

        internal static Vector Scale
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

        internal static bool IsDraw
        {
            get => _meshes[0].IsDraw;
            set
            {
                foreach (var mesh in _meshes)
                {
                    mesh.IsDraw = value;
                }
            }
        }

        private static int _selectedMeshIndex = -1;

        private static TransformSpace _transformSpace = TransformSpace.World;

        internal static void Update()
        {
            if (_attachMesh == null)
            {
                IsDraw = false;
                return;
            }

            IsDraw = true;

            if (Mouse.IsButtonPressed(MouseButton.Left) && (Mouse.CursorState == CursorState.Confined))
            {
                _selectedMeshIndex = GetSelectedMeshIndex();
            }
            else if (Mouse.IsButtonReleased(MouseButton.Left) || (Mouse.CursorState != CursorState.Confined))
            {
                _selectedMeshIndex = -1;
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

            Scale = (Vector)(Vector.Distance(Location, CameraManager.ActiveCamera.Location) / 75.0f);
        }

        private static Vector GetMovement()
        {
            float speed = Scale.X * 0.2f;

            return _selectedMeshIndex switch
            {
                0 => Transform.Right * Mouse.Delta.X * speed,
                1 => Transform.Up * -Mouse.Delta.Y * speed,
                2 => Transform.Forward * Mouse.Delta.X * speed,
                3 => (Transform.Right * Mouse.Delta.X + Transform.Up * -Mouse.Delta.Y) * speed,
                _ => Vector.Zero
            };
        }

        private static int GetSelectedMeshIndex()
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

        internal static bool TryAttachTo(Mesh mesh)
        {
            if (mesh is not GizmoMesh
                && Mouse.IsButtonPressed(MouseButton.Left) 
                && Mouse.CursorState == CursorState.Confined
                && mesh.IsMouseOn(out _, out _)
            )
            {
                _attachMesh = mesh;
                return true;
            }

            return false;
        }

        internal static void Detach()
        {
            _attachMesh = null;
        }
    }
}
