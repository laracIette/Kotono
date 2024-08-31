using Kotono.Graphics.Objects.Texts;
using Kotono.Utils.Coordinates;
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
                RelativePosition = Rect.RelativePosition,
                RelativeSize = new Point(25.0f, 30.0f),
                Layer = 2,
                Spacing = 0.6f,
                Parent = this
            };
        }
    }
}
