using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Linq;
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

        private static Mesh? ActiveMesh => ISelectable.Active as Mesh;

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

        internal static bool IsSelected => _selectedMeshIndex != -1;

        private static TransformSpace _transformSpace = TransformSpace.World;

        internal static void Update()
        {
            if (ActiveMesh == null)
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

            Location = ActiveMesh.Location;

            switch (_transformSpace)
            {
                case TransformSpace.World:
                    Rotation = Vector.Zero;
                    break;

                case TransformSpace.Local:
                    Rotation = ActiveMesh.Rotation;
                    break;

                default:
                    break;
            }

            var movement = GetMovement();

            Location += movement;

            foreach (var obj in ISelectable.Selected.OfType<IObject3D>())
            {
                obj.Location += movement;
            }

            Scale = (Vector)(Vector.Distance(Location, CameraManager.ActiveCamera.Location) / 75.0f);
        }

        private static Vector GetMovement()
        {
            float speed = Scale.X * 0.2f;

            return _selectedMeshIndex switch
            {
                0 => speed * Mouse.Delta.X * Transform.Right,
                1 => speed * -Mouse.Delta.Y * Transform.Up,
                2 => speed * Mouse.Delta.X * Transform.Forward,
                3 => speed * (Mouse.Delta.X * Transform.Right + -Mouse.Delta.Y * Transform.Up),
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
                    // Select the mesh that is the closest to the camera
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestMesh = i;
                    }
                }
            }

            return closestMesh;
        }
    }
}
