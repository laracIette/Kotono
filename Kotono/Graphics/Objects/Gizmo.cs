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

        internal static Transform Transform { get; private set; } = Transform.Default;

        internal static Vector Location
        {
            get => Transform.RelativeLocation;
            set
            {
                Transform.RelativeLocation = value;
                foreach (var mesh in _meshes)
                {
                    mesh.RelativeLocation = value;
                }
            }
        }

        internal static Rotator Rotation
        {
            get => Transform.RelativeRotation;
            set
            {
                Transform.RelativeRotation = value;
                foreach (var mesh in _meshes)
                {
                    mesh.RelativeRotation = value;
                }
            }
        }

        internal static Vector Scale
        {
            get => Transform.RelativeScale;
            set
            {
                Transform.RelativeScale = value;
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

        private static readonly TransformSpace _transformSpace = TransformSpace.Relative;

        private static readonly GizmoMode _gizmoMode = GizmoMode.Rotation;

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
                UpdateSelectedMeshIndex();
            }
            else if (Mouse.IsButtonReleased(MouseButton.Left) || (Mouse.CursorState != CursorState.Confined))
            {
                _selectedMeshIndex = -1;
            }

            Location = ActiveMesh.WorldLocation;

            switch (_transformSpace)
            {
                case TransformSpace.World:
                    Rotation = Rotator.Zero;
                    break;

                case TransformSpace.Relative:
                    //Rotation = ActiveMesh.RelativeRotation;
                    break;
            }

            switch (_gizmoMode)
            {
                case GizmoMode.Location:
                    var locDelta = GetLocationDelta();

                    if (!locDelta.IsZero)
                    {
                        Location += locDelta;

                        foreach (var selected3D in ISelectable.Selected.OfType<IObject3D>())
                        {
                            selected3D.RelativeLocation += locDelta;
                        }
                    }
                    break;

                case GizmoMode.Rotation:
                    var rotDelta = GetRotationDelta();

                    if (!rotDelta.IsZero)
                    {
                        Rotation += rotDelta;

                        foreach (var selected3D in ISelectable.Selected.OfType<IObject3D>())
                        {
                            selected3D.RelativeRotation += rotDelta;
                        }
                    }
                    break;

                case GizmoMode.Scale: 
                    break;
            }

            Scale = (Vector)(Vector.Distance(Location, ObjectManager.ActiveCamera.Location) / 75.0f);
            
            var delta = Rotator.Zero;
            if (Keyboard.IsKeyDown(Keys.Left))
            {
                delta.Pitch = Keyboard.IsKeyDown(Keys.LeftShift) ? -Time.Delta : Time.Delta;
            }
            if (Keyboard.IsKeyDown(Keys.Down))
            {
                delta.Yaw = Keyboard.IsKeyDown(Keys.LeftShift) ? -Time.Delta : Time.Delta;
            }
            if (Keyboard.IsKeyDown(Keys.Right))
            {
                delta.Roll = Keyboard.IsKeyDown(Keys.LeftShift) ? -Time.Delta : Time.Delta;
            }

            if (!delta.IsZero)
            {
                Rotation += delta;

                Printer.Print(Rotation, true);
            }
        }

        private static Vector GetLocationDelta()
        {
            float speed = Scale.X * 0.1f;

            float delta = Mouse.Delta.X - Mouse.Delta.Y;

            return _selectedMeshIndex switch
            {
                0 => speed * delta * Transform.Right,
                1 => speed * delta * Transform.Up,
                2 => speed * delta * Transform.Forward,
                3 => speed * (Mouse.Delta.X * Vector.Right - Mouse.Delta.Y * Vector.Up),
                _ => Vector.Zero
            };
        }

        private static Rotator GetRotationDelta()
        {
            float speed = Scale.X * 0.1f;

            float delta = Mouse.Delta.X - Mouse.Delta.Y;

            return _selectedMeshIndex switch
            {
                0 => speed * delta * Rotator.UnitPitch,
                1 => speed * delta * Rotator.UnitYaw,
                2 => speed * delta * Rotator.UnitRoll,
                3 => Rotator.Zero,
                _ => Rotator.Zero
            };
        }

        private static void UpdateSelectedMeshIndex()
        {
            float closestDistance = float.PositiveInfinity;
            _selectedMeshIndex = -1;

            for (int i = 0; i < _meshes.Length; i++)
            {
                if (_meshes[i].IsHovered)
                {
                    // Select the mesh that is the closest to the camera
                    if (_meshes[i].IntersectionDistance < closestDistance)
                    {
                        closestDistance = _meshes[i].IntersectionDistance;
                        _selectedMeshIndex = i;
                    }
                }
            }
        }
    }
}
