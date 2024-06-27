namespace Kotono.Graphics.Objects.Lights
{
    internal abstract class Light<T> : Object3D<T>, ILight where T : LightSettings
    {
        public bool IsOn
        {
            get => _settings.IsOn;
            set => _settings.IsOn = value;
        }

        public float Power
        {
            get => _settings.Power;
            set => _settings.Power = value;
        }

        internal Light(T settings)
            : base(settings)
        {
        }
    }
}
