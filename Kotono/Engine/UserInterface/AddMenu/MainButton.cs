using Kotono.Graphics.Objects.Buttons;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;

namespace Kotono.Engine.UserInterface.AddMenu
{
    internal class MainButton : TextButton
    {
        private readonly SubMenu _subMenu;

        internal MainButton(string[] options)
        {
            _subMenu = new SubMenu(options)
            {
                Parent = this
            };

            RelativeSize = new Point(100.0f, 100.0f);
            Color = Color.Gray;
            Layer = 1;
            TargetCornerSize = 25.0f;
            TargetFallOff = 2.0f;
        }

        public override void OnPressed()
        {
            _subMenu.IsDraw = true;

            _subMenu.RelativePosition = Anchor switch
            {
                Anchor.TopLeft => Rect.TopLeft,
                Anchor.TopRight => Rect.TopRight,
                Anchor.BottomLeft => Rect.BottomLeft,
                Anchor.BottomRight => Rect.BottomRight,
                _ => throw new SwitchException(typeof(Anchor), Anchor)
            };
        }
    }
}
