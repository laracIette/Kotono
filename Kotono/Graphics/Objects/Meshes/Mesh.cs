using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Meshes
{
    internal abstract class Mesh : Object3D, IMesh
    {
        private readonly List<Hitbox> _hitboxes;

        protected readonly Object3DShader _shader;

        internal Model Model { get; }

        internal Material Material { get; set; } = new();

        internal bool IsGravity { get; set; } = false;

        internal CollisionState CollisionState { get; set; }

        public override Rotator RelativeRotationVelocity
        {
            get => base.RelativeRotationVelocity;
            set => base.RelativeRotationVelocity = value;
        }

        public bool IsUpdateFizix { get; set; } = false;

        public float LastIntersectionCheckTime { get; private set; } = 0.0f;

        public float IntersectionDistance { get; private set; } = 0.0f;

        public Vector IntersectionLocation { get; private set; } = Vector.Zero;

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
                        if (Intersection.IntersectRayTriangle(Camera.Active.Location, Mouse.Ray, in triangle, Transform, out Vector intersectionLocation, out float intersectionDistance))
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

        public override Shader Shader => NewLightingShader.Instance;

        internal Mesh(string shader, List<Hitbox> hitboxes, string model)
        {
            _shader = (Object3DShader)ShaderManager.Shaders[shader];

            _hitboxes = hitboxes;

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Transform.Parent = Transform;
                hitbox.Color = Color.Red;

                hitbox.EnterCollision += (s, e) => OnEnterCollision(e);
                hitbox.ExitCollision += (s, e) => OnExitCollision(e);
            }

            Model = Model.Load(new ModelSettings { Path = model, Shader = _shader });
        }

        public override void Update()
        {
            var tempLoc = RelativeLocation;

            if (IsGravity)
            {
                tempLoc += Time.Delta * Fizix.Gravity;
            }

            foreach (var hitbox in _hitboxes)
            {
                hitbox.RelativeLocation = tempLoc;

                if ((CollisionState == CollisionState.BlockAll) && hitbox.IsColliding)
                {
                    hitbox.RelativeLocation = RelativeLocation;
                    tempLoc = RelativeLocation;
                }
            }

            RelativeLocation = tempLoc;
        }

        protected virtual void OnLeftButtonPressed()
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
                // If mesh isn't clicked and left control is up
                else if (!Keyboard.IsKeyDown(Keys.LeftControl))
                {
                    IsSelected = false;
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
        }

        public override void Draw()
        {
            var color = IsSelected ? (IsActive ? Color.Green : Color.Orange) : Color;
            
            if (Shader is NewLightingShader newLightingShader)
            {
                newLightingShader.SetModel(Transform.Model);
                newLightingShader.SetBaseColor(color);
                newLightingShader.SetMaterial(Material);
            }

            Material.Use();

            Model.Draw();

            Texture.Bind(0);
        }

        protected virtual void OnEnterCollision(CollisionEventArgs collision) { }

        protected virtual void OnExitCollision(CollisionEventArgs collision) { }

        public override void Dispose()
        {
            foreach (var hitbox in _hitboxes)
            {
                hitbox.Dispose();
            }

            base.Dispose();
        }
    }
}