using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight : IDrawable
    {
        private readonly Mesh _mesh;

        public Vector Location => _mesh.Location;

        public bool IsDraw { get; private set; }

        public bool IsGravity
        {
            get => _mesh.IsGravity;
            set => _mesh.IsGravity = value;
        }

        public Color Color { get; protected set; }

        public Color Ambient { get; protected set; }

        public Color Specular { get; protected set; }

        public float Constant { get; protected set; }

        public float Linear { get; protected set; }

        public float Quadratic { get; protected set; }

        public const int MAX_COUNT = 100;

        public PointLight(Vector location, Color ambient, Color diffuse, Color specular, float constant, float linear, float quadratic)
        {
            Ambient = ambient;
            Color = diffuse;
            Specular = specular;
            Constant = constant;
            Linear = linear;
            Quadratic = quadratic;

            _mesh = new PointLightMesh(location, this);

            ObjectManager.Create(this);
        }

        public virtual void Update()
        {

        }

        public void Draw()
        {

        }

        public void Save()
        {

        }

        public void Show()
        {
            IsDraw = true;
        }

        public void Hide()
        {
            IsDraw = false;
        }

        public void Delete()
        {
            ObjectManager.Delete(this);
        }

        public void Dispose()
        {
            _mesh.Delete();

            GC.SuppressFinalize(this);
        }
    }
}