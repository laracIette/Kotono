using Kotono.Input;
using Kotono.Utils.Coordinates;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal abstract class Button : RoundedBox, IButton
    {
        private readonly ButtonEventArgs _args = new();

        internal EventHandler<ButtonEventArgs>? Pressed { get; set; }

        internal EventHandler<ButtonEventArgs>? Released { get; set; }

        public bool IsDown { get; private set; }

        public bool WasDown { get; private set; }

        public bool IsPressed => IsDown && !WasDown;

        public bool IsReleased => !IsDown && WasDown;

        public override void Update()
        {
            WasDown = IsDown && Mouse.WasButtonDown(MouseButton.Left);

            IsDown = IsDraw && Mouse.IsButtonDown(MouseButton.Left) && Rect.Overlaps(Rect, Mouse.Position);

            if (IsPressed)
            {
                OnPressed();
            }
            else if (IsReleased)
            {
                OnReleased();
            }
        }

        public virtual void OnPressed()
        {
            Pressed?.Invoke(this, _args);
        }

        public virtual void OnReleased()
        {
            Released?.Invoke(this, _args);
        }
    }
}
