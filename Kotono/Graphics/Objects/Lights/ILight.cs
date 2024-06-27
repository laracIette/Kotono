namespace Kotono.Graphics.Objects.Lights
{
    internal interface ILight : IObject3D
    {
        public bool IsOn { get; set; }

        public float Power { get; set; }
    }
}
