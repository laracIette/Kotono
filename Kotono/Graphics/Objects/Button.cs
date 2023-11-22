using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Graphics.Objects
{
    public class Button : RoundedBox
    {
        public event EventHandler? Pressed;

        public Button(Rect dest, Color color, int layer, float fallOff, float cornerSize) 
            : base(dest, color, layer, fallOff, cornerSize)
        { }

        public override void Update()
        {
            base.Update();

            if (IsDraw && Mouse.IsButtonPressed(MouseButton.Left) && Rect.Overlaps(Dest, Mouse.RelativePosition))
            {
                OnPressed();
            }
        }

        protected virtual void OnPressed()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
