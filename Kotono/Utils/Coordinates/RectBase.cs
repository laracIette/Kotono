namespace Kotono.Utils.Coordinates
{
    public struct RectBase(Point position, Point baseSize, Point scale)
    {
        public Point Position { get; set; } = position;

        public Point BaseSize { get; set; } = baseSize;

        public Point Scale { get; set; } = scale;

        public readonly Point Size => BaseSize * Scale;

        public RectBase(Point position, Point baseSize) : this(position, baseSize, Point.Unit) { }

        public static RectBase operator +(RectBase left, in RectBase right)
        {
            left.Position += right.Position;
            left.Scale += right.Scale - Point.Unit;
            return left;
        }

        public static RectBase operator *(in float f, RectBase t)
        {
            t.Position = f * t.Position;
            t.Scale = Point.Unit + f * (t.Scale - Point.Unit);
            return t;
        }

        public static RectBase operator /(RectBase t, in float f)
        {
            t.Position /= f;
            t.Scale = Point.Unit + (t.Scale - Point.Unit) / f;
            return t;
        }
    }
}
