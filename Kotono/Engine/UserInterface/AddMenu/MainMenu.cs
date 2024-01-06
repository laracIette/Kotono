using Kotono.Engine.UserInterface.AddMenu.MainButtons;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Settings;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine.UserInterface.AddMenu
{
    public static class MainMenu
    {
        private static readonly RoundedBox _backgroundBox = new(
            new RoundedBoxSettings
            {
                Dest = new Rect(0.0f, 0.0f, 300.0f, 300.0f),
                Color = Color.DarkGray,
                FallOff = 2.0f,
                CornerSize = 30.0f
            }
        );

        private static readonly MainButton[] _buttons =
        {
            new Objects2DButton(),
            new Objects3DButton(),
            new LightsButton(),
            new TriggersButton()
        };

        public static bool IsDraw
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

        public static Point Position
        {
            get => _backgroundBox.Position;
            set
            {
                _backgroundBox.Position = value;
                _buttons[0].Dest = Rect.FromAnchor(new Rect(value + (-5.0f, -5.0f), 140.0f, 140.0f), Anchor.BottomRight);
                _buttons[1].Dest = Rect.FromAnchor(new Rect(value + (5.0f, -5.0f), 140.0f, 140.0f), Anchor.BottomLeft);
                _buttons[2].Dest = Rect.FromAnchor(new Rect(value + (-5.0f, 5.0f), 140.0f, 140.0f), Anchor.TopRight);
                _buttons[3].Dest = Rect.FromAnchor(new Rect(value + (5.0f, 5.0f), 140.0f, 140.0f), Anchor.TopLeft);
            }
        }

        static MainMenu()
        {
            IsDraw = false;
        }

        public static void Update()
        {
            if (Keyboard.IsKeyPressed(Keys.A) && Keyboard.IsKeyDown(Keys.LeftShift) && Mouse.CursorState == CursorState.Confined)
            {
                Position = Mouse.Position;
                IsDraw = true;
            }

            if (IsDraw)
            {
                if (Mouse.IsButtonPressed(MouseButton.Left) && !Rect.Overlaps(_backgroundBox.Dest, Mouse.Position))
                {
                    IsDraw = false;
                }
            }
        }
    }
}
