using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class TextButtonEventArgs : ButtonEventArgs
    {
        internal string Text { get; set; } = "";

        internal TextButtonEventArgs()
            : base() { }
    }
}
