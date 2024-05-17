using Kotono.Graphics.Objects.Texts;
using Kotono.Settings;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class TextButtonList
    {
        private readonly TextButton[] _buttons;

        internal TextButtonList(TextButtonListSettings settings)
        {
            _buttons = new TextButton[settings.Texts.Length];

            var positions = Rect.FromAnchor(settings.Texts.Length, settings.Rect.Position, settings.Rect.Size, Anchor.Center);

            for (int i = 0; i < settings.Texts.Length; i++)
            {
                _buttons[i] = new TextButton(
                    new TextButtonSettings
                    {
                        Color = settings.Color,
                        CornerSize = settings.CornerSize,
                        FallOff = settings.FallOff,
                        Layer = settings.Layer,
                        Rect = new Rect(positions[i], settings.Rect.Size),
                        TextSettings = new TextSettings
                        {
                            Text = settings.Texts[i]
                        }
                    }
                );

                _buttons[i].Pressed += OnPressed;
            }
        }

        private void OnPressed(object? sender, TextButtonEventArgs e)
        {
            Printer.Print(e.Text, true);
        }
    }
}
