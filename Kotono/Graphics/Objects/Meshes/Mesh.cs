using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Shaders;
using Kotono.Graphics.Textures;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Graphics.Objects.Meshes
{
    internal abstract class Mesh : Object3D, IMesh
    {
        private Model _model = new(Path.FromAssets(@"Default\model.obj"));

        public override Shader Shader
        {
            get => base.Shader;
            set => Model.SetVertexAttributesLayout(base.Shader = value);
        }

        public CustomList<Hitbox> Hitboxes { get; } = [];

        public Model Model
        {
            get => _model;
            set => (_model = value).SetVertexAttributesLayout(Shader);
        }

        public Material Material { get; set; } = new();

        internal bool IsGravity { get; set; } = false;

        internal CollisionState CollisionState { get; set; }

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
                        if (Intersection.IntersectRayTriangle(Camera.Active.WorldLocation, Mouse.Ray, in triangle, Transform, out Vector intersectionLocation, out float intersectionDistance))
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

        internal Mesh()
        {
            Hitboxes.AddAction = h =>
            {
                h.Parent = this;

                h.EnterCollision = (s, e) => OnEnterCollision(e);
                h.ExitCollision = (s, e) => OnExitCollision(e);
            };

            Hitboxes.RemoveAction = h => h.Dispose();
        }

        protected virtual void OnEnterCollision(CollisionEventArgs collision) { }

        protected virtual void OnExitCollision(CollisionEventArgs collision) { }

        public virtual void UpdateFizix() { }

        public override void Update()
        {
            if (IsGravity)
            {
                RelativeLocationVelocity = Fizix.Gravity;
            }
        }

        private void OnLeftButtonPressed()
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
                        ISelectable3D.Selected.Remove(this);
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
            if (ISelectable3D.Selected.Contains(this))
            {
                // If mesh isn't selected
                if (!IsSelected)
                {
                    ISelectable3D.Selected.Remove(this);
                }
            }
            // If mesh isn't in ISelectable.Selected and is selected
            else if (IsSelected)
            {
                ISelectable3D.Selected.Add(this);
            }
        }

        public override void UpdateShader()
        {
            var mixColor = IsSelected ? (IsActive ? Color.Green : Color.Orange) : Color.Black;
            var color = Color.Blend(Color, mixColor);

            if (Shader is LightingPBRShader lightingPBRShader)
            {
                lightingPBRShader.SetModel(Transform.Model);
                lightingPBRShader.SetBaseColor(color);
                lightingPBRShader.SetMaterial(Material);
            }
        }

        public override void Draw()
        {
            Material.Use();

            Model.Draw();

            ITexture.Unbind(TextureTarget.Texture2D);
        }

        public override void Dispose()
        {
            Hitboxes.ForEach(h => h.Dispose()); 

            base.Dispose();
        }
    }
}