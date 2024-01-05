﻿using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Graphics.Objects.Buttons
{
    internal abstract class Button(Rect dest, Color color, int layer, float fallOff, float cornerSize)
        : RoundedBox(dest, color, layer, fallOff, cornerSize),
        IButton
    {
        public bool IsDown { get; private set; }

        public bool WasDown { get; private set; }

        public bool IsPressed => IsDown && !WasDown;

        public bool IsReleased => !IsDown && WasDown;

        public override void Update()
        {
            WasDown = Mouse.WasButtonDown(MouseButton.Left) && IsDown;

            IsDown = IsDraw && Mouse.IsButtonDown(MouseButton.Left) && Rect.Overlaps(Dest, Mouse.Position);
            
            if (IsPressed)
            {
                OnPressed();
            }
            else if (IsReleased)
            {
                OnReleased();
            }
        }

        public virtual void OnPressed() { }

        public virtual void OnReleased() { }
    }
}
