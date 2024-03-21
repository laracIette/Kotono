using Kotono.Graphics.Objects.Meshes;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
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

        private static IObject3D? ActiveMesh => ISelectable.Active as IObject3D;

        private static readonly Transform _transform = Transform.Default;

        internal static Transform Transform => _transform;

        internal static Vector Location
        {
            get => _transform.RelativeLocation;
            set
            {
                _transform.RelativeLocation = value;
                foreach (var mesh in _meshes)
                {
                    mesh.RelativeLocation = value;
                }
            }
        }

        internal static Vector Rotation
        {
            get => _transform.RelativeRotation;
            set
            {
                _transform.RelativeRotation = value;
                foreach (var mesh in _meshes)
                {
                    mesh.RelativeRotation = value;
                }
            }
        }

        internal static Vector Scale
        {
            get => _transform.RelativeScale;
            set
            {
                _transform.RelativeScale = value;
                foreach (var mesh in _meshes)
                {
                    mesh.RelativeScale = value;
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

        private static readonly TransformSpace _transformSpace = TransformSpace.World;

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

            Location = ActiveMesh.WorldLocation;

            switch (_transformSpace)
            {
                case TransformSpace.World:
                    Rotation = Vector.Zero;
                    break;

                case TransformSpace.Local:
                    Rotation = ActiveMesh.WorldRotation;
                    break;

                default:
                    break;
            }

            var movement = GetMovement();

            Location += movement;

            foreach (var obj in ISelectable.Selected.OfType<IObject3D>())
            {
                obj.RelativeLocation += movement;
            }

            Scale = (Vector)(Vector.Distance(Location, ObjectManager.ActiveCamera.Location) / 75.0f);
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
                if (_meshes[i].IsHovered)
                {
                    // Select the mesh that is the closest to the camera
                    if (_meshes[i].IntersectionDistance < closestDistance)
                    {
                        closestDistance = _meshes[i].IntersectionDistance;
                        closestMesh = i;
                    }
                }
            }

            return closestMesh;
        }
    }
}
