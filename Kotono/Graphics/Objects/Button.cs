using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Graphics.Objects
{
    public class Button : RoundedBox
    {
        // TODO: replace by event ?
        public bool IsPressed { get; private set; }

        public Button(Rect dest, Color color, int layer, float fallOff, float cornerSize) :
            base(dest, color, layer, fallOff, cornerSize)
        { }

        public override void Update()
        {
            base.Update();

            IsPressed = Mouse.IsButtonPressed(MouseButton.Left) && Rect.Overlaps(Dest, Mouse.RelativePosition);
        }
    }
}
