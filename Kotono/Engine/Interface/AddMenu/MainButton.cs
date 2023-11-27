﻿using Kotono.Graphics.Objects;
using Kotono.Utils;
using System;

namespace Kotono.Engine.Interface.AddMenu
{
    public class MainButton : TextButton
    {
        private readonly SubMenu _subMenu;

        private readonly Anchor _anchor;

        public MainButton(string text, string[] options, Anchor anchor)
            : base(new Rect(100), Color.Gray, 1, 2, 25, text)
        {
            _subMenu = new SubMenu(options, anchor);
            _anchor = anchor;
        }

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
                Anchor.TopLeft => new Point(Dest.X + Dest.W, Dest.Y),
                Anchor.TopRight => new Point(Dest.X - Dest.W, Dest.Y),
                Anchor.BottomLeft => new Point(Dest.X + Dest.W, Dest.Y),
                Anchor.BottomRight => new Point(Dest.X - Dest.W, Dest.Y),
                _ => throw new Exception($"error: Anchor \"{_anchor}\" isn't supported")
            };
        }
    }
}
