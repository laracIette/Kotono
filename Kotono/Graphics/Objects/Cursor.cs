using Kotono.Input;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal class Cursor()
        : Image(
              new ImageSettings
              {
                  Texture = Utils.Path.ASSETS + @"coke.png",
                  Rect = new Rect(Point.Zero, new Point(50.0f, 50.0f)),
                  Layer = int.MaxValue
              }
          )
    {
        public override void Update()
        {
            Position = Mouse.Position + 25.0f;
        }
    }
}
