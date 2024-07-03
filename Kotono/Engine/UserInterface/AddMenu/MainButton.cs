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
                Rect = new Rect(Point.Zero, new Point(100.0f, 100.0f)),
                Color = Color.Gray,
                Layer = 1,
                TextSettings = new TextSettings { Source = text },
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
                Anchor.TopLeft => Rect.TopLeft,
                Anchor.TopRight => Rect.TopRight,
                Anchor.BottomLeft => Rect.BottomLeft,
                Anchor.BottomRight => Rect.BottomRight,
                _ => throw new SwitchException(typeof(Anchor), _anchor)
            };

            base.OnPressed();
        }
    }
}
