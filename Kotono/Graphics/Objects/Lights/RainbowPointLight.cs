using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Lights
{
    internal class RainbowPointLight()
        : PointLight(
            new PointLightSettings
            {
                RelativeLocation = Random.Vector(-20.0f, 20.0f),
                Ambient = Color.White / 20.0f
            }
        )
    {
        public override void Update()
        {
            base.Update();

            Color = Color.Rainbow(0.002f);

            LocationVelocity += Random.Vector(-0.01f, 0.01f);

            LocationVelocity = Vector.Clamp(LocationVelocity, -1.0f, 1.0f);

            RelativeLocation += LocationVelocity * Time.Delta;
        }
    }
}
