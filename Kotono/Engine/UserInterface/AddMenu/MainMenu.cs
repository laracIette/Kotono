using Kotono.Engine.UserInterface.AddMenu.MainButtons;
using Kotono.Graphics.Objects;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine.UserInterface.AddMenu
{
    internal class MainMenu : Object
    {
        private readonly RoundedBox _backgroundBox = new()
        {
            Size = new Point(300.0f, 300.0f),
            Color = Color.DarkSlateGray,
            FallOff = 2.0f,
            CornerSize = 30.0f
        };

        private readonly MainButton[] _buttons =
        [
            new Objects2DButton(),
            new Objects3DButton(),
            new LightsButton(),
            new TriggersButton()
        ];

        internal bool IsDraw
        {
            get => _backgroundBox.IsDraw;
            set
            {
                _backgroundBox.IsDraw = value;
                foreach (var button in _buttons)
                {
                    button.IsDraw = value;
                }
            }
        }

        internal Point Position
        {
            get => _backgroundBox.Position;
            set
            {
                _backgroundBox.Position = value;

                var size = new Point(140.0f, 140.0f);

                _buttons[0].Position = Rect.GetPositionFromAnchor(value, size, Anchor.BottomRight, 5.0f);
                _buttons[1].Position = Rect.GetPositionFromAnchor(value, size, Anchor.BottomLeft, 5.0f);
                _buttons[2].Position = Rect.GetPositionFromAnchor(value, size, Anchor.TopRight, 5.0f);
                _buttons[3].Position = Rect.GetPositionFromAnchor(value, size, Anchor.TopLeft, 5.0f);
            }
        }

        internal MainMenu()
        {
            IsDraw = false;
        }

        private void OnAKeyPressed()
        {
            if (Mouse.CursorState == CursorState.Confined && Keyboard.IsKeyDown(Keys.LeftShift))
            {
                Position = Mouse.Position;
                IsDraw = true;
            }
        }

        private void OnLeftButtonPressed()
        {
            if (IsDraw)
            {
                if (!Rect.Overlaps(_backgroundBox.Rect, Mouse.Position))
                {
                    IsDraw = false;
                }
            }
        }
    }
}