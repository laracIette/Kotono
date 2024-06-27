using Kotono.Graphics.Objects.Meshes;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Linq;
using CursorState = Kotono.Input.CursorState;

namespace Kotono.Graphics.Objects
{
    internal class Gizmo : Object3D
    {
        private static readonly Gizmo _instance = new();

        private readonly GizmoMesh[] _meshes =
        [
            new GizmoMesh("x"),
            new GizmoMesh("y"),
            new GizmoMesh("z"),
            new GizmoMesh("sphere")
        ];

        private static IObject3D? ActiveMesh => ISelectable.Active as IObject3D;
        
        public override Vector WorldLocation
        {
            get => Transform.WorldLocation;
            set
            {
                Transform.WorldLocation = value;
                foreach (var mesh in _meshes)
                {
                    mesh.WorldLocation = value;
                }
            }
        }

        public override Rotator WorldRotation
        {
            get => Transform.WorldRotation;
            set
            {
                Transform.WorldRotation = value;
                foreach (var mesh in _meshes)
                {
                    mesh.WorldRotation = value;
                }
            }
        }

        public override Vector WorldScale
        {
            get => Transform.WorldScale;
            set
            {
                Transform.WorldScale = value;
                foreach (var mesh in _meshes)
                {
                    mesh.WorldScale = value;
                }
            }
        }

        public override bool IsDraw
        {
            get
            {
                foreach (var mesh in _meshes)
                {
                    mesh.IsDraw = IsUpdate;
                }
                return IsUpdate;
            }
        }

        public override bool IsUpdate 
        { 
            get => ActiveMesh != null;
            set { }
        }

        private static int _selectedMeshIndex = -1;

        internal static new bool IsSelected => _selectedMeshIndex != -1;

        private readonly TransformSpace _transformSpace = TransformSpace.Relative;

        private readonly GizmoMode _gizmoMode = GizmoMode.Rotation;

        private Gizmo() : base() { }

        public override void Update()
        {
            if (Mouse.CursorState != CursorState.Confined)
            {
                _selectedMeshIndex = -1;
            }

            WorldLocation = ActiveMesh!.WorldLocation;

            switch (_transformSpace)
            {
                case TransformSpace.World:
                    WorldRotation = Rotator.Zero;
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
                        WorldLocation += locDelta;

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
                        WorldRotation += rotDelta;

                        foreach (var selected3D in ISelectable.Selected.OfType<IObject3D>())
                        {
                            selected3D.RelativeRotation += rotDelta;
                        }
                    }
                    break;

                case GizmoMode.Scale:
                    break;
            }

            WorldScale = (Vector)(Vector.Distance(WorldLocation, Camera.Active.Location) / 75.0f);

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
                WorldRotation += delta;

                //Printer.Print(WorldRotation, true);
            }
        }

        private Vector GetLocationDelta()
        {
            float speed = WorldScale.X * 0.1f;

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

        private Rotator GetRotationDelta()
        {
            float speed = WorldScale.X * 0.1f;

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

        private void UpdateSelectedMeshIndex()
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

        private void OnLeftButtonPressed()
        {
            if (IsUpdate && Mouse.CursorState == CursorState.Confined)
            {
                UpdateSelectedMeshIndex();
            }
        }

        protected virtual void OnLeftButtonReleased()
        {
            _selectedMeshIndex = -1;
        }
    }
}
