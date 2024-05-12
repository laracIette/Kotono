using Kotono.Graphics.Objects.Buttons;
using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;

namespace Kotono.Engine.UserInterface.AddMenu
{
    internal class MainButton(string text, string[] options, Anchor anchor)
        : TextButton(
            new TextButtonSettings
            {
                Rect = new Rect(Point.Zero, 100.0f, 100.0f), // 100 so that CornerSize doesn't get restricted
                Color = Color.Gray,
                Layer = 1,
                TextSettings = new TextSettings { Text = text },
                CornerSize = 25.0f,
                FallOff = 2.0f
            }
        )
    {
        private readonly SubMenu _subMenu = new(options, anchor);

        private readonly Anchor _anchor = anchor;

        public override bool IsDraw
        {
            get => base.IsDraw;
            set
            {
                base.IsDraw = value;
                if (!value)
                {
                    _subMenu.IsDraw = false;
                }
            }
        }

        public override void OnPressed()
        {
            _subMenu.IsDraw = true;

            _subMenu.Position = _anchor switch
            {
                Anchor.TopLeft => new Point(Rect.Position.X + Rect.Size.X / 2.0f, Rect.Position.Y - Rect.Size.Y / 2.0f),
                Anchor.TopRight => new Point(Rect.Position.X - Rect.Size.X / 2.0f, Rect.Position.Y - Rect.Size.Y / 2.0f),
                Anchor.BottomLeft => new Point(Rect.Position.X + Rect.Size.X / 2.0f, Rect.Position.Y + Rect.Size.Y / 2.0f),
                Anchor.BottomRight => new Point(Rect.Position.X - Rect.Size.X / 2.0f, Rect.Position.Y + Rect.Size.Y / 2.0f),
                _ => throw new SwitchException(typeof(Anchor), _anchor)
            };

            base.OnPressed();
        }
    }
}
