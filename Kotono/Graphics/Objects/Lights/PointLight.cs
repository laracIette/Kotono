using Kotono.Utils;

namespace Kotono.Graphics.Objects.Lights
{
    internal class PointLight : Light<PointLightSettings>
    {
        internal const int MAX_COUNT = 100;

        internal Color Ambient
        {
            get => _settings.Ambient;
            set => _settings.Ambient = value;
        }

        internal Color Specular
        {
            get => _settings.Specular;
            set => _settings.Specular = value;
        }

        internal float Constant
        {
            get => _settings.Constant;
            set => _settings.Constant = value;
        }

        internal float Linear
        {
            get => _settings.Linear;
            set => _settings.Linear = value;
        }

        internal float Quadratic
        {
            get => _settings.Quadratic;
            set => _settings.Quadratic = value;
        }

        public override Color Color
        {
            get => Parent?.Color ?? base.Color;
            set
            {
                base.Color = value;

                if (Parent != null)
                {
                    Parent.Color = value;
                }
            }
        }

        internal PointLight(PointLightSettings settings)
            : base(settings)
        {
        }
    }
}