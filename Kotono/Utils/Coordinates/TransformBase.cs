#pragma warning disable CS0618 // TransformBase.operator *(TransformBase, float)

namespace Kotono.Utils.Coordinates
{
    public struct TransformBase(Vector location, Rotator rotation, Vector scale)
    {
        public Vector Location { get; set; } = location;

        public Rotator Rotation { get; set; } = rotation;

        public Vector Scale { get; set; } = scale;

        public static TransformBase operator +(TransformBase left, in TransformBase right)
        {
            left.Location += right.Location;
            left.Rotation += right.Rotation;
            left.Scale += right.Scale - Vector.Unit;
            return left;
        }

        public static TransformBase operator *(TransformBase t, in float f)
        {
            t.Location *= f;
            t.Rotation *= f;
            t.Scale = Vector.Unit + (t.Scale - Vector.Unit) * f;
            return t;
        }

        public static TransformBase operator /(TransformBase t, in float f)
        {
            t.Location /= f;
            t.Rotation /= f;
            t.Scale = Vector.Unit + (t.Scale - Vector.Unit) / f;
            return t;
        }
    }
}
