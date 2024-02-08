using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class ButtonList
    {
        private readonly Button[] _buttons;

        internal ButtonList(Button[] buttons)
        {
            _buttons = buttons;
          
            foreach (var button in _buttons)
            {
                button.Pressed += OnPressed;
            }
        }

        private void OnPressed(object? sender, ButtonEventArgs e)
        {

        }
    }
}
