using Kotono.Utils;

namespace Kotono.Graphics.Objects.Lights
{
    internal abstract class Light : Object3D, ILight
    {
        public bool IsOn { get; set; } = true;

        public float Intensity { get; set; } = 1.0f;

        public Color Ambient { get; set; }

        public Color Diffuse { get; set; }

        public Color Specular { get; set; }

        public void Switch() => IsOn = !IsOn;
    }
}
