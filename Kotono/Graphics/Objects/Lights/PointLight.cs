using Kotono.Utils;

namespace Kotono.Graphics.Objects.Lights
{
    internal class PointLight : Light
    {
        internal const int MAX_COUNT = 100;

        internal Color Ambient { get; set; }

        internal Color Specular { get; set; }

        internal float Constant { get; set; }

        internal float Linear { get; set; }

        internal float Quadratic { get; set; }
    }
}