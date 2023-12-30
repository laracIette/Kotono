using Kotono.Engine.UserInterface.AddMenu.MainButtons;
using Kotono.Graphics.Objects;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine.UserInterface.AddMenu
{
    public static class MainMenu
    {
        private static readonly RoundedBox _backgroundBox = new(new Rect(0.0f, 0.0f, 300.0f, 300.0f), Color.DarkGray, 0, 2.0f, 30.0f);

        private static readonly MainButton[] _buttons =
        {
            new Objects2DButton(),
            new Objects3DButton(),
            new LightsButton(),
            new TriggersButton()
        };

        public static bool IsDraw => _backgroundBox.IsDraw;

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
            _backgroundBox.Hide();

            foreach (var button in _buttons)
            {
                button.Hide();
            }
        }

        public static void Update()
        {
            if (Keyboard.IsKeyPressed(Keys.A) && Keyboard.IsKeyDown(Keys.LeftShift))
            {
                Position = Mouse.Position;
                Show();
            }

            if (IsDraw)
            {
                if (Mouse.IsButtonPressed(MouseButton.Left) && !Rect.Overlaps(_backgroundBox.Dest, Mouse.Position))
                {
                    Hide();
                }
            }
        }

        private static void Show()
        {
            _backgroundBox.Show();
            foreach (var button in _buttons)
            {
                button.Show();
            }
        }

        private static void Hide()
        {
            _backgroundBox.Hide();
            foreach (var button in _buttons)
            {
                button.Hide();
            }
        }
    }
}
