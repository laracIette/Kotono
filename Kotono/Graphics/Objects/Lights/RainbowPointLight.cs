﻿using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Lights
{
    internal class RainbowPointLight()
        : PointLight(
            new PointLightSettings
            {
                Transform = new Transform
                {
                    RelativeLocation = Random.Vector(-20.0f, 20.0f),
                },
                Ambient = Color.White / 20.0f
            }
        )
    {
        public override void Update()
        {
            Color = Color.Rainbow(0.002f);

            RelativeLocationVelocity += Random.Vector(-0.1f, 0.1f);

            RelativeLocationVelocity = Vector.Clamp(RelativeLocationVelocity, -1.0f, 1.0f);
        }
    }
}
