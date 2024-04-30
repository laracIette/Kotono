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

        internal Rect Dest => new Rect(Mouse.Position, Size);

        internal Brush(BrushSettings settings)
            : base()
        { 
            Name = settings.Name;
            Size = settings.Size;
            _texture = new Image(
                new ImageSettings
                {
                    Texture = Path.ASSETS + @"brushes\" + Name
                }
            );
        }
    }
}
