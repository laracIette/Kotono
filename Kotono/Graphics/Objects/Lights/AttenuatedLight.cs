namespace Kotono.Graphics.Objects.Lights
{
    internal abstract class AttenuatedLight : Light, IAttenuatedLight
    {
        public float Constant { get; set; } = 1.0f;

        public float Linear { get; set; }

        public float Quadratic { get; set; }
    }
}
