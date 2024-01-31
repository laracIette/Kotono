using Kotono.File;
using Kotono.Utils;
using Random = Kotono.Utils.Random;

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

            Velocity += Random.Vector(-0.1f, 0.1f) * Time.Delta;

            Velocity = Vector.Clamp(Velocity, -0.001f, 0.001f);

            Location += Velocity;
        }
    }
}
