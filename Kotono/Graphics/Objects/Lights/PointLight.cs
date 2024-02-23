using Kotono.Settings;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Lights
{
    internal class PointLight : Object3D
    {
        internal const int MAX_COUNT = 100;

        private readonly PointLightMesh _mesh = new();

        public override Vector Location
        {
            get => _mesh.Location;
            set => _mesh.Location = value;
        }

        internal bool IsGravity
        {
            get => _mesh.IsGravity;
            set => _mesh.IsGravity = value;
        }

        internal Color Ambient { get; set; }

        internal Color Specular { get; set; }

        internal float Constant { get; set; }

        internal float Linear { get; set; }

        internal float Quadratic { get; set; }

        internal PointLight(PointLightSettings settings)
            : base(settings)
        {
            Ambient = settings.Ambient;
            Color = settings.Color;
            Specular = settings.Specular;
            Constant = settings.Constant;
            Linear = settings.Linear;
            Quadratic = settings.Quadratic;

            _mesh.AttachTo(this);
        }

        public override void Delete()
        {
            _mesh.Delete();

            base.Delete();
        }
    }
}