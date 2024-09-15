using Kotono.Graphics.Textures;
using Kotono.Input;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal sealed class Cursor : Image
    {
        internal Cursor()
        {
            Texture = new ImageTexture(Path.FromAssets(@"coke.png"));
            WorldSize = new Point(50.0f, 50.0f);
            Layer = int.MaxValue;
        }

        public override void Update()
        {
            WorldPosition = 25.0f + Mouse.Position;
        }
    }
}
