using Kotono.Utils;

namespace Kotono.Graphics.Objects.Lights
{
    internal abstract class Light : Object3D, ILight
    {
        public bool IsOn { get; set; } = true;

        public float Power { get; set; } = 1.0f;

        internal Color Diffuse
        {
            get => Color;
            set => Color = value;
        }

        public void SwitchOnOff()
        {
            IsOn = !IsOn;
        }
    }
}
