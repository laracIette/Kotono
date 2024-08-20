using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class TextButtonList
    {
        private readonly TextButton[] _buttons;

        internal TextButtonList(string[] texts, Point position, Point size, Color color, float cornerSize, float fallOff, int layer)
        {
            _buttons = new TextButton[texts.Length];

            var positions = Rect.GetPositionFromAnchor(texts.Length, position, size, Anchor.Center);

            for (int i = 0; i < texts.Length; i++)
            {
                _buttons[i] = new TextButton
                {
                    Position = positions[i],
                    Size = size,
                    Source = texts[i],
                    Color = color,
                    CornerSize = cornerSize,
                    FallOff = fallOff,
                    Layer = layer,
                    Pressed = OnPressed
                };
            }
        }

        private void OnPressed(object? sender, TextButtonEventArgs e)
        {
            Printer.Print(e.Source, true);
        }
    }
}
