using Kotono.Graphics.Objects;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine.Interface
{
    public static class AddMenu
    {
        private static readonly RoundedBox _backgroundBox = new(new Rect(0, 0, 300, 300), Color.DarkGray, 0, 5, 30);

        private static readonly Button[] _buttons =
        {
            new(Rect.FromAnchor(new Rect(0, 0, 140, 140), Anchor.BottomRight), Color.Gray, 1, 2, 25), // 2D Objects
            new(Rect.FromAnchor(new Rect(0, 0, 140, 140), Anchor.BottomLeft),  Color.Gray, 1, 2, 25), // 3D Objects
            new(Rect.FromAnchor(new Rect(0, 0, 140, 140), Anchor.TopRight),    Color.Gray, 1, 2, 25), // Lights
            new(Rect.FromAnchor(new Rect(0, 0, 140, 140), Anchor.TopLeft),     Color.Gray, 1, 2, 25)  // Trigger
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
        }

        public static void Update()
        {
            if (Keyboard.IsKeyPressed(Keys.A) && Keyboard.IsKeyDown(Keys.LeftShift))
            {
                Position = Mouse.RelativePosition;
                Show();
            }

            if (IsDraw)
            {
                if (Mouse.IsButtonPressed(MouseButton.Left) && !Rect.Overlaps(_backgroundBox.Dest, Mouse.RelativePosition))
                {
                    Hide();
                }

                foreach (var button in _buttons)
                {
                    if (button.IsPressed)
                    {
                        KT.Print("oui");
                        break;
                    }
                }
            }
        }

        private static void Show()
        {
            _backgroundBox.Show();
        }

        private static void Hide()
        {
            _backgroundBox.Hide();
        }
    }
}
