using Kotono.Utils;
using Random = Kotono.Utils.Random;

namespace Kotono.Graphics.Objects.Lights
{
    public class RainbowPointLight()
        : PointLight(
              Random.Vector(-20.0f, 20.0f),
              Color.White / 20.0f,
              Color.White,
              Color.White,
              1.0f,
              0.09f,
              0.032f
          )
    {
        public override void Update()
        {
            base.Update();

            Color = Color.Rainbow(0.002);
            
            Velocity += Random.Vector(-0.1f, 0.1f) * Time.DeltaS;

            Velocity = Vector.Clamp(Velocity, -0.001f, 0.001f);

            Location += Velocity;
        }
    }
}
