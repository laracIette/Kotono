using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Buttons
{
    internal sealed class TextButtonList : RoundedBox
    {
        private readonly TextButton[] _buttons;

        public override int Layer
        {
            get => base.Layer;
            set
            {
                base.Layer = value;
                foreach (var button in _buttons)
                {
                    button.Layer = value + 1;
                }
            }
        }

        internal TextButtonList(TextButton[] buttons, Point size)
        {
            _buttons = buttons;

            var positions = new Point[buttons.Length];
            Rect.GetPositionsFromAnchor(positions, Point.Zero, size, Anchor.Center);

            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];

                button.RelativePosition = positions[i];
                button.Pressed = (s, e) => Printer.PrintRainbow(e.Source, 0.01f);
                button.Parent = this;
            }
        }
    }
}
