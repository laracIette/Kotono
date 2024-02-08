using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class ButtonEventArgs : EventArgs
    {
        internal string Name { get; set; } = "";

        internal ButtonEventArgs()
            : base() { }
    }
}
