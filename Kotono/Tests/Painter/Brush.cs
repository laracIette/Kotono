using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Textures;
using Kotono.Input;
using Kotono.Utils.Coordinates;

namespace Kotono.Tests.Painter
{
    internal sealed class Brush : Object
    {
        private readonly Image _texture;

        internal string Name { get; }

        internal Point Size { get; set; }

        internal Rect Rect => new(Mouse.Position, Size);

        internal Brush(string name)
        {
            Name = name;

            _texture = new Image
            {
                Texture = new ImageTexture(Path.FromAssets(@"brushes\" + name))
            };
        }
    }
}
