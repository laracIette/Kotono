using Kotono.Engine.UserInterface.AddMenu.MainButtons;
using Kotono.Graphics.Objects;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine.UserInterface.AddMenu
{
    internal sealed class MainMenu : Object2D
    {
        private readonly RoundedBox _backgroundBox;

        private readonly MainButton[] _buttons;

        internal MainMenu()
        {
            IsDraw = false;

            _backgroundBox = new RoundedBox()
            {
                RelativeSize = new Point(300.0f, 300.0f),
                Color = Color.DarkSlateGray,
                TargetFallOff = 2.0f,
                TargetCornerSize = 30.0f,
                Parent = this
            };

            var size = new Point(140.0f, 140.0f);
            float offset = 5.0f;

            _buttons =
            [
                new Objects2DButton
                {
                    RelativePosition = Rect.GetPositionFromAnchor(Point.Zero, size, Anchor.BottomRight, offset),
                    Parent = this
                },
                new Objects3DButton
                {
                    RelativePosition = Rect.GetPositionFromAnchor(Point.Zero, size, Anchor.BottomLeft, offset),
                    Parent = this
                },
                new LightsButton
                {
                    RelativePosition = Rect.GetPositionFromAnchor(Point.Zero, size, Anchor.TopRight, offset),
                    Parent = this
                },
                new TriggersButton
                {
                    RelativePosition = Rect.GetPositionFromAnchor(Point.Zero, size, Anchor.TopLeft, offset),
                    Parent = this
                }
            ];
        }

        private void OnAKeyPressed()
        {
            if (Mouse.CursorState == CursorState.Confined && Keyboard.IsKeyDown(Keys.LeftShift))
            {
                RelativePosition = Mouse.Position;
                IsDraw = true;
            }
        }

        private void OnLeftButtonPressed()
        {
            if (IsDraw)
            {
                if (!_backgroundBox.Rect.Overlaps(Mouse.Position))
                {
                    IsDraw = false;
                }
            }
        }
    }
}