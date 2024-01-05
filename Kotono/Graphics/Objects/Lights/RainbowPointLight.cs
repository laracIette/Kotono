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
        }
    }
}
