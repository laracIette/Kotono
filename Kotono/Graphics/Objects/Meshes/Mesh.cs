using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;
using Kotono.Utils.Coordinates;
using System;

namespace Kotono.Graphics.Objects.Meshes
{
    internal abstract class Mesh : Object3D, IMesh
    {
        private readonly List<Hitbox> _hitboxes;

        protected readonly Shader _shader;

        internal MeshModel Model { get; }

        internal Material Material { get; }

        internal bool IsGravity { get; set; } = false;

        internal CollisionState CollisionState { get; set; }

        public override Vector RotationVelocity
        {
            get => Vector.Deg(base.RotationVelocity);
            set => base.RotationVelocity = Vector.Rad(value);
        }

        public bool IsFizix { get; set; } = false;

        public float LastIntersectionCheckTime { get; private set; } = 0.0f;

        public Vector IntersectionLocation { get; private set; } = Vector.Zero;

        public float IntersectionDistance { get; private set; } = 0.0f;

        public override bool IsHovered
        {
            get
            {
                if (Time.Now - LastIntersectionCheckTime > IMesh.IntersectionCheckFrequency)
                {
                    LastIntersectionCheckTime = Time.Now;

                    IntersectionLocation = Vector.Zero;
                    IntersectionDistance = 0.0f;

                    foreach (var triangle in Model.Triangles)
                    {
                        triangle.Transform = Transform;

                        if (Intersection.IntersectRayTriangle(ObjectManager.ActiveCamera.Location, Mouse.Ray, triangle, out Vector intersectionLocation, out float intersectionDistance))
                        {
                            IntersectionLocation = intersectionLocation;
                            IntersectionDistance = intersectionDistance;

                            return true;
                        }
                    }
                }

                return false;
            }
        }

        internal Mesh(MeshSettings settings)
            : base(settings)
        {
            _hitboxes = settings.Hitboxes;
            Color = settings.Color;

            Material = new Material(settings.MaterialTexturesSettings);

            _shader = ShaderManager.Get(settings.Shader);

            _hitboxes = [new Sphere(new HitboxSettings())];
            foreach (var hitbox in _hitboxes)
            {
                hitbox.Location = Location;
                hitbox.Rotation = Vector.Zero;
                hitbox.Scale = Scale * 5.0f;
                hitbox.Color = Color.Red;

                hitbox.EnterCollision += OnEnterCollision;
                hitbox.ExitCollision += OnExitCollision;
            }

            Model = MeshModel.Load(new MeshModelSettings { Path = settings.Model, Shader = _shader });
        }

        public override void Update()
        {
            var tempLoc = Location;

            if (IsGravity)
            {
                tempLoc += Fizix.Gravity * Time.Delta;
            }

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Location = tempLoc;

                if ((CollisionState == CollisionState.BlockAll) && hitbox.IsColliding)
                {
                    hitbox.Location = Location;
                    tempLoc = Location;
                }
            }

            Location = tempLoc;

            if (Mouse.IsButtonPressed(MouseButton.Left))
            {
                OnMouseLeftButtonPressed();
            }
        }

        public void UpdateFizix()
        {
            Fizix.Update(this);
        }

        public override void Draw()
        {
            Material.Use();

            _shader.SetMatrix4("model", Transform.Model);
            _shader.SetColor("color", Color);

            Model.Draw();
        }

        private void OnMouseLeftButtonPressed()
        {
            // If gizmo isn't selected
            if (!Gizmo.IsSelected)
            {
                // If mesh is clicked
                if (IsHovered)
                {
                    // If left control is down
                    if (Keyboard.IsKeyDown(Keys.LeftControl))
                    {
                        IsSelected = true;
                        ISelectable.Selected.Remove(this);
                    }
                    // If left control is up
                    else
                    {
                        IsSelected = !IsSelected;
                    }
                }

                // If mesh isn't clicked
                else
                {
                    // If left control is up
                    if (!Keyboard.IsKeyDown(Keys.LeftControl))
                    {
                        IsSelected = false;
                    }
                }
            }

            // If mesh is in ISelectable.Selected
            if (ISelectable.Selected.Contains(this))
            {
                // If mesh isn't selected
                if (!IsSelected)
                {
                    ISelectable.Selected.Remove(this);
                }
            }
            // If mesh isn't in ISelectable.Selected and is selected
            else if (IsSelected)
            {
                ISelectable.Selected.Add(this);
            }

            Color = IsSelected ? (IsActive ? Color.Green : Color.Orange) : Color.White;
        }

        public override void Save()
        {
            if (_settings is MeshSettings settings)
            {
                settings.Model = Model.Path;
                settings.Shader = _shader.Name;
                settings.MaterialTexturesSettings = Material.MaterialTexturesSettings;
            }

            base.Save();
        }

        private void OnEnterCollision(object? sender, HitboxEventArgs e)
        {
            OnEnterCollision(e.Source, e.Collider);
        }

        protected virtual void OnEnterCollision(IHitbox source, IHitbox collider) { }

        private void OnExitCollision(object? sender, HitboxEventArgs e)
        {
            OnExitCollision(e.Source, e.Collider);
        }

        protected virtual void OnExitCollision(IHitbox source, IHitbox collider) { }

        public override void Delete()
        {
            foreach (var hitbox in _hitboxes)
            {
                hitbox.Delete();
            }

            base.Delete();
        }
    }
}