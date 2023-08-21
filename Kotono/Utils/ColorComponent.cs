namespace Kotono.Utils
{
    public struct ColorComponent
    {
        private float _value;

        public float Value
        { 
            readonly get => _value; 
            set => _value = Math.Clamp(value, 0, 1);
        }

        public ColorComponent()
        {
            Value = 0;
        }

        public ColorComponent(float f)
        {
            Value = f;
        }

        public static implicit operator float(ColorComponent c) 
        {
            return c.Value;
        }

        public static implicit operator ColorComponent(float f) 
        {
            return new ColorComponent(f);
        }
    }
}
