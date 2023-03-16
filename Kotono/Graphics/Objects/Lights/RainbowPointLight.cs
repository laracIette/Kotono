using Kotono.Utils;
using OpenTK.Mathematics;
using System;
using Random = Kotono.Utils.Random;

namespace Kotono.Graphics.Objects.Lights
{
    public class RainbowPointLight : PointLight
    {
        public RainbowPointLight()
            : base(
                  Random.Vector3(-20.0f, 20.0f),
                  new Vector3(0.05f),
                  new Vector3(1.0f),
                  new Vector3(1.0f),
                  1.0f,
                  0.09f,
                  0.032f
              )
        {
        }

        public override void Update()
        {
            base.Update();

            _diffuse.X = (float)(Math.Sin(0.002 * Time.Now + 0.0) * 0.5) + 0.5f;
            _diffuse.Y = (float)(Math.Sin(0.002 * Time.Now + 2.0) * 0.5) + 0.5f;
            _diffuse.Z = (float)(Math.Sin(0.002 * Time.Now + 4.0) * 0.5) + 0.5f;
        }
    }
}
