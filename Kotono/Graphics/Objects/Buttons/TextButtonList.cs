using Kotono.Settings;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class TextButtonList
    {
        private readonly TextButton[] _buttons;

        internal TextButtonList(TextButtonListSettings settings)
        {
            _buttons = new TextButton[settings.Texts.Length];

            for (int i = 0; i < settings.Texts.Length; i++)
            {
                _buttons[i] = new TextButton(
                    new TextButtonSettings
                    {
                        Color = settings.Color,
                        CornerSize = settings.CornerSize,
                        FallOff = settings.FallOff,
                        Layer = settings.Layer,
                        Dest = new Rect(),
                        
                    }
                );

                _buttons[i].Pressed += OnPressed;
            }
        }

        private void OnPressed(object? sender, TextButtonEventArgs e)
        {
            KT.Print(e.Text, true);
        }
    }
}
