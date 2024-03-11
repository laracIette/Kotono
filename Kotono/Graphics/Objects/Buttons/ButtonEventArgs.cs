using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class ButtonEventArgs()
        : EventArgs()
    {
        /// <summary>
        /// The time at which the event occured.
        /// </summary>
        internal float Time { get; } = Utils.Time.Now;
    }
}
