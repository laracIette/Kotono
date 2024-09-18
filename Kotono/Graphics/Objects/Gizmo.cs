using Kotono.Graphics.Objects.Meshes;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Windowing.GraphicsLibraryFramework;
using CursorState = Kotono.Input.CursorState;

namespace Kotono.Graphics.Objects
{
    internal sealed class Gizmo : Object3D
    {
        private Gizmo()
        {
            foreach (var mesh in _meshes)
            {
                mesh.Parent = this;
            }
        }

        private static readonly Gizmo _instance = new();

        private readonly GizmoMesh[] _meshes =
        [
            new GizmoMesh
            {
                Color = Color.Red,
                Model = new Model(Path.FromAssets($@"Gizmo\gizmo_x.obj"))
            },
            new GizmoMesh
            {
                Color = Color.Green,
                Model = new Model(Path.FromAssets($@"Gizmo\gizmo_y.obj"))
            },
            new GizmoMesh
            {
                Color = Color.Blue,
                Model = new Model(Path.FromAssets($@"Gizmo\gizmo_z.obj"))
            },
            new GizmoMesh
            {
                Color = Color.White,
                Model = new Model(Path.FromAssets($@"Gizmo\gizmo_shpere.obj"))
            }
        ];

        public override bool IsDraw => IsUpdate;

        public override bool IsUpdate => ISelectable3D.Active is not null;

        private static int _selectedMeshIndex = -1;

        internal static new bool IsSelected => _selectedMeshIndex != -1;

        private readonly CoordinateSpace _transformSpace = CoordinateSpace.Relative;

        private readonly GizmoMode _gizmoMode = GizmoMode.Location;

        public override void Update()
        {
            if (Mouse.CursorState != CursorState.Confined)
            {
                _selectedMeshIndex = -1;
            }

            WorldLocation = ISelectable3D.Active!.WorldLocation;

            switch (_transformSpace)
            {
                case CoordinateSpace.World:
                    WorldRotation = Rotator.Zero;
                    break;

                case CoordinateSpace.Relative:
                    //Rotation = ActiveMesh.RelativeRotation;
                    break;
            }

            switch (_gizmoMode)
            {
                case GizmoMode.Location:
                    var locDelta = GetLocationDelta();

                    if (!Vector.IsNullOrZero(locDelta))
                    {
                        WorldLocation += locDelta;

                        foreach (var selected3D in ISelectable3D.Selected)
                        {
                            selected3D.RelativeLocation += locDelta;
                        }
                    }
                    break;

                case GizmoMode.Rotation:
                    var rotDelta = GetRotationDelta();

                    if (!Rotator.IsNullOrZero(rotDelta))
                    {
                        WorldRotation += rotDelta;

                        foreach (var selected3D in ISelectable3D.Selected)
                        {
                            selected3D.RelativeRotation += rotDelta;
                        }
                    }
                    break;

                case GizmoMode.Scale:
                    break;
            }

            WorldScale = (Vector)(Vector.Distance(WorldLocation, Camera.Active.WorldLocation) / 75.0f);

            float pitch = 0.0f;
            float yaw = 0.0f;
            float roll = 0.0f;

            if (Keyboard.IsKeyDown(Keys.Left))
            {
                pitch = Keyboard.IsKeyDown(Keys.LeftShift) ? -Time.Delta : Time.Delta;
            }
            if (Keyboard.IsKeyDown(Keys.Down))
            {
                yaw = Keyboard.IsKeyDown(Keys.LeftShift) ? -Time.Delta : Time.Delta;
            }
            if (Keyboard.IsKeyDown(Keys.Right))
            {
                roll = Keyboard.IsKeyDown(Keys.LeftShift) ? -Time.Delta : Time.Delta;
            }

            var delta = new Rotator(pitch, yaw, roll);

            if (!Rotator.IsNullOrZero(delta))
            {
                WorldRotation += delta;
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

        private void OnLeftButtonReleased()
        {
            _selectedMeshIndex = -1;
        }
    }
}
