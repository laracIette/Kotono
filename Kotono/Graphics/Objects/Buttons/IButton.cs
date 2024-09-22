using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal interface IButton
    {
        /// <summary>
        /// The event that occurs when the <see cref="IButton"/> is pressed.
        /// </summary>
        public EventHandler<ButtonEventArgs>? Pressed { get; set; }

        /// <summary>
        /// The event that occurs when the <see cref="IButton"/> is released.
        /// </summary>
        public EventHandler<ButtonEventArgs>? Released { get; set; }

        /// <summary>
        /// Wether the <see cref="IButton"/> is down at the current frame.
        /// </summary>
        public bool IsDown { get; }

        /// <summary>
        /// Wether the <see cref="IButton"/> was down at the precedent frame.
        /// </summary>
        public bool WasDown { get; }

        /// <summary>
        /// Wether the <see cref="IButton"/> is down since the current frame.
        /// </summary>
        public bool IsPressed { get; }

        /// <summary>
        /// Wether the <see cref="IButton"/> is up since the current frame.
        /// </summary>
        public bool IsReleased { get; }

        /// <summary>
        /// Called when the <see cref="IButton"/> is pressed.
        /// </summary>
        public void OnPressed();

        /// <summary>
        /// Called when the <see cref="IButton"/> is released.
        /// </summary>
        public void OnReleased();
    }
}
