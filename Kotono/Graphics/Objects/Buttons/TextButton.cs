using Kotono.Graphics.Objects.Texts;
using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class TextButton : Button
    {
        internal Text Text { get; }

        internal new EventHandler<TextButtonEventArgs>? Pressed { get; set; }

        internal TextButton()
        {
            Text = new Text
            {
                Parent = this,
                Layer = Layer + 1
            };
        }
    }
}
