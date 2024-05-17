using OpenTK.Mathematics;

namespace Kotono.Utils.Coordinates
{
    internal readonly record struct Sides(float Left, float Right, float Top, float Bottom)
    {
        public static explicit operator Vector4(Sides s)
        {
            return new Vector4(s.Left, s.Right, s.Top, s.Bottom);
        }
    }
}
