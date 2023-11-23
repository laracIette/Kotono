using Kotono.Graphics.Objects;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Engine.Interface
{
    public static class AddMenu
    {
        private static readonly RoundedBox _backgroundBox = new(new Rect(0, 0, 300, 300), Color.DarkGray, 0, 2, 30);

        private static readonly TextButton[] _buttons =
        {
            new(Rect.FromAnchor(new Rect(0, 0, 140, 140), Anchor.BottomRight), Color.Gray, 1, 2, 25, "2D Objects"),
            new(Rect.FromAnchor(new Rect(0, 0, 140, 140), Anchor.BottomLeft),  Color.Gray, 1, 2, 25, "3D Objects"), 
            new(Rect.FromAnchor(new Rect(0, 0, 140, 140), Anchor.TopRight),    Color.Gray, 1, 2, 25, "Lights"), 
            new(Rect.FromAnchor(new Rect(0, 0, 140, 140), Anchor.TopLeft),     Color.Gray, 1, 2, 25, "Triggers")  
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

            _buttons[0].Pressed += On2DObjectsButtonPressed;
            _buttons[1].Pressed += On3DObjectsButtonPressed;
            _buttons[2].Pressed += OnLightsButtonPressed;
            _buttons[3].Pressed += OnTriggersButtonPressed;

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

        private static void On2DObjectsButtonPressed(object? sender, EventArgs e)
        {
            KT.Print("2dobjects");
        }

        private static void On3DObjectsButtonPressed(object? sender, EventArgs e)
        {
            KT.Print("3dobjects");
        }

        private static void OnLightsButtonPressed(object? sender, EventArgs e)
        {
            KT.Print("lights");
        }

        private static void OnTriggersButtonPressed(object? sender, EventArgs e)
        {
            KT.Print("triggers");
        }
    }
}
