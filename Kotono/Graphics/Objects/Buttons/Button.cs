using Kotono.Input;
using Kotono.Utils.Coordinates;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal abstract class Button : RoundedBox, IButton
    {
        public EventHandler<ButtonEventArgs>? Pressed { get; set; }

        public EventHandler<ButtonEventArgs>? Released { get; set; }

        public bool IsDown { get; private set; }

        public bool WasDown { get; private set; }

        public bool IsPressed => IsDown && !WasDown;

        public bool IsReleased => !IsDown && WasDown;

        public override void Update()
        {
            WasDown = IsDown;

            IsDown = IsDraw && Mouse.IsButtonDown(MouseButton.Left) && Rect.Overlaps(Rect, Mouse.Position);

            if (IsPressed)
            {
                OnPressed();
                Pressed?.Invoke(this, new ButtonEventArgs());
            }
            else if (IsReleased)
            {
                OnReleased();
                Released?.Invoke(this, new ButtonEventArgs());
            }
        }

        public virtual void OnPressed() { }

        public virtual void OnReleased() { }
    }
}
