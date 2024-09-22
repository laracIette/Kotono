using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Lights
{
    internal sealed class RainbowPointLight : PointLight
    {
        internal RainbowPointLight()
        {
            RelativeLocation = Random.Vector(-20.0f, 20.0f);
            Ambient = Color.White / 20.0f;
        }

        public override void Update()
        {
            Diffuse = Color.Rainbow(0.002f);
            Color = Diffuse;

            RelativeLocationVelocity += Random.Vector(-0.1f, 0.1f);

            RelativeLocationVelocity = Vector.MinLength(RelativeLocationVelocity, 10.0f);
        }
    }
}
