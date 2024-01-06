using Kotono.Graphics.Objects.Settings;
using Kotono.Input;
using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    internal class Cursor()
        : Image(
              new ImageSettings
              {
                  Path = Utils.Path.Assets + @"coke.png",
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
