using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Buttons
{
    internal sealed class TextButtonList : RoundedBox
    {
        private readonly TextButton[] _buttons;

        private Color _buttonsColor;

        internal Color ButtonsColor
        {
            get => _buttonsColor;
            set
            {
                _buttonsColor = value;
                foreach (var button in _buttons)
                {
                    button.Color = value;
                }
            }
        }

        internal TextButtonList(string[] texts, Point position, Point size, float cornerSize, float fallOff, int layer)
        {
            _buttons = new TextButton[texts.Length];

            var positions = new Point[texts.Length];
            Rect.GetPositionsFromAnchor(positions, position, size, Anchor.Center);

            for (int i = 0; i < texts.Length; i++)
            {
                _buttons[i] = new TextButton
                {
                    RelativePosition = positions[i],
                    RelativeSize = size,
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
