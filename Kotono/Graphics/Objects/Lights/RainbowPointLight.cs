using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Lights
{
    internal class RainbowPointLight()
        : PointLight(
            new PointLightSettings
            {
                Location = Random.Vector(-20.0f, 20.0f),
                Ambient = Color.White / 20.0f
            }
        )
    {
        public override void Update()
        {
            base.Update();

            Color = Color.Rainbow(0.002f);

            LocationVelocity += Random.Vector(-0.1f, 0.1f) * Time.Delta;

            LocationVelocity = Vector.Clamp(LocationVelocity, -0.001f, 0.001f);

            Location += LocationVelocity;
        }
    }
}
