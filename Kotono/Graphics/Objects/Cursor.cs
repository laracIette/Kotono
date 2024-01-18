﻿using Kotono.File;
using Kotono.Input;
using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    internal class Cursor()
        : Image(
              new ImageSettings
              {
                  Texture = Utils.Path.ASSETS + @"coke.png",
                  Dest = new Rect(0.0f, 0.0f, 50.0f, 50.0f),
                  Layer = 10
              }
          )
    {
        public override void Update()
        {
            Position = Mouse.Position + 25.0f;
        }
    }
}
