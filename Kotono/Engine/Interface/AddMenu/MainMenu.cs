using Kotono.Engine.Interface.AddMenu.MainButtons;
using Kotono.Graphics.Objects;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine.Interface.AddMenu
{
    public static class MainMenu
    {
        private static readonly RoundedBox _backgroundBox = new(new Rect(0, 0, 300, 300), Color.DarkGray, 0, 2, 30);

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
                _buttons[0].Dest = Rect.FromAnchor(new Rect(value + (-5, -5), 140, 140), Anchor.BottomRight);
                _buttons[1].Dest = Rect.FromAnchor(new Rect(value + (5, -5), 140, 140), Anchor.BottomLeft);
                _buttons[2].Dest = Rect.FromAnchor(new Rect(value + (-5, 5), 140, 140), Anchor.TopRight);
                _buttons[3].Dest = Rect.FromAnchor(new Rect(value + (5, 5), 140, 140), Anchor.TopLeft);
            }
        }

        public static void Init()
        {
            _backgroundBox.Hide();

            foreach (var button in _buttons)
            {
                button.Init();
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
