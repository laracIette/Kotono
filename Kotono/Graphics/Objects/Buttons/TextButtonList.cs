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
                    RelativePosition = positions[i],
                    RelativeSize = size,
                    Color = color,
                    TargetCornerSize = cornerSize,
                    TargetFallOff = fallOff,
                    Layer = layer,
                    Pressed = (s, e) => Printer.Print(e.Source, true)
                };
                _buttons[i].Text.Source = texts[i];
            }
        }
    }
}
