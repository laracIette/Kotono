using Kotono.Graphics.Objects;
using Kotono.Utils;
using System;

namespace Kotono.Engine.UserInterface.AddMenu
{
    public class MainButton(string text, string[] options, Anchor anchor)
        : TextButton(new Rect(100), Color.Gray, 1, 2f, 25f, text)
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

        public override void Update()
        {
            base.Update();
            _text.Update();
        }

        protected override void OnPressed()
        {
            _subMenu.IsDraw = true;
            _subMenu.Position = _anchor switch
            {
                Anchor.TopLeft => new Point(Dest.X + Dest.W / 2.0f, Dest.Y - Dest.H / 2.0f),
                Anchor.TopRight => new Point(Dest.X - Dest.W / 2.0f, Dest.Y - Dest.H / 2.0f),
                Anchor.BottomLeft => new Point(Dest.X + Dest.W / 2.0f, Dest.Y + Dest.H / 2.0f),
                Anchor.BottomRight => new Point(Dest.X - Dest.W / 2.0f, Dest.Y + Dest.H / 2.0f),
                _ => throw new Exception($"error: Anchor \"{_anchor}\" isn't supported")
            };
        }
    }
}
