using Kotono.Graphics.Objects;
using Kotono.Utils;
using System;

namespace Kotono.Engine.Interface.AddMenu
{
    public class MainButton(string text, string[] options, Anchor anchor)
        : TextButton(new Rect(100), Color.Gray, 1, 2, 25, text)
    {
        private readonly SubMenu _subMenu = new(options, anchor);

        private readonly Anchor _anchor = anchor;

        public override void Init()
        {
            base.Init();
            _subMenu.Init();
        }

        public override void Update()
        {
            base.Update();
            _text.Update();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            _subMenu.Hide();
        }

        protected override void OnPressed()
        {
            _subMenu.Show();
            _subMenu.Position = _anchor switch
            {
                Anchor.TopLeft => new Point(Dest.X + Dest.W / 2, Dest.Y - Dest.H / 2),
                Anchor.TopRight => new Point(Dest.X - Dest.W / 2, Dest.Y - Dest.H / 2),
                Anchor.BottomLeft => new Point(Dest.X + Dest.W / 2, Dest.Y + Dest.H / 2),
                Anchor.BottomRight => new Point(Dest.X - Dest.W / 2, Dest.Y + Dest.H / 2),
                _ => throw new Exception($"error: Anchor \"{_anchor}\" isn't supported")
            };
        }
    }
}
