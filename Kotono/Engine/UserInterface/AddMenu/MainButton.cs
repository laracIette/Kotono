using Kotono.Graphics.Objects.Buttons;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;

namespace Kotono.Engine.UserInterface.AddMenu
{
    internal class MainButton : TextButton
    {
        private readonly SubMenu _subMenu;

        internal Anchor Anchor { get; set; }

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

        public MainButton(string[] options, Anchor anchor)
        {
            _subMenu = new SubMenu(options, anchor);
            Anchor = anchor;
            Size = new Point(100.0f, 100.0f);
            Color = Color.Gray;
            Layer = 1;
            CornerSize = 25.0f;
            FallOff = 2.0f;
        }

        public override void OnPressed()
        {
            _subMenu.IsDraw = true;

            _subMenu.Position = Anchor switch
            {
                Anchor.TopLeft => Rect.TopLeft,
                Anchor.TopRight => Rect.TopRight,
                Anchor.BottomLeft => Rect.BottomLeft,
                Anchor.BottomRight => Rect.BottomRight,
                _ => throw new SwitchException(typeof(Anchor), Anchor)
            };

            base.OnPressed();
        }
    }
}
