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
        private readonly RoundedBox _backgroundBox = new(
            new RoundedBoxSettings
            {
                Rect = new Rect(0.0f, 0.0f, 300.0f, 300.0f),
                Color = Color.DarkSlateGray,
                FallOff = 2.0f,
                CornerSize = 30.0f
            }
        );

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
                _buttons[0].Rect = Rect.FromAnchor(new Rect(value, 140.0f, 140.0f), Anchor.BottomRight, 5.0f);
                _buttons[1].Rect = Rect.FromAnchor(new Rect(value, 140.0f, 140.0f), Anchor.BottomLeft, 5.0f);
                _buttons[2].Rect = Rect.FromAnchor(new Rect(value, 140.0f, 140.0f), Anchor.TopRight, 5.0f);
                _buttons[3].Rect = Rect.FromAnchor(new Rect(value, 140.0f, 140.0f), Anchor.TopLeft, 5.0f);
            }
        }

        internal MainMenu() 
            : base()
        {
            IsDraw = false;
        }

        public override void Update()
        {
            if (Mouse.IsButtonPressed(MouseButton.Left))
            {
                OnLeftButtonPressed();
            }
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