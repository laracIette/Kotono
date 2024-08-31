using Kotono.Input;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal class Cursor : Image
    {
        internal Cursor()
            : base(Path.FromAssets(@"coke.png"))
        {
            WorldSize = new Point(50.0f, 50.0f);
            Layer = int.MaxValue;
        }

        public override void Update()
        {
            WorldPosition = Mouse.Position + 25.0f;
        }
    }
}
