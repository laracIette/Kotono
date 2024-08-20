using Kotono.Graphics.Objects;
using Kotono.Input;
using Kotono.Utils.Coordinates;

namespace Kotono.Tests.Painter
{
    internal class Brush : Object
    {
        private readonly Image _texture;

        internal string Name { get; }

        internal Point Size { get; set; }

        internal Rect Rect => new(Mouse.Position, Size);

        internal Brush(string name)
        {
            Name = name;
            _texture = new Image(Path.FromAssets(@"brushes\" + name));
        }
    }
}
