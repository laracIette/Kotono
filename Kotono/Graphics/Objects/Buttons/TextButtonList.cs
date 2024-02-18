using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class TextButtonList
    {
        private readonly TextButton[] _buttons;

        internal TextButtonList(TextButton[] buttons)
        {
            _buttons = buttons;
          
            foreach (var button in _buttons)
            {
                button.Pressed += OnPressed;
            }
        }

        private void OnPressed(object? sender, TextButtonEventArgs e)
        {
            KT.Print(e.Text);
        }
    }
}
