using Kotono.Graphics.Objects;
using Kotono.Utils;
using System;
using System.Collections.Generic;

namespace Kotono.Engine.Interface.AddMenu
{
    public class SubMenu
    {
        private readonly List<Text> _options = new();

        private readonly Anchor _anchor;

        public Point Position
        {
            set
            {
                for (int i = 0; i < _options.Count; i++)
                {
                    _options[i].Position = GetTextDest(i, value).Position;
                }
            }
        }

        public SubMenu(string[] options, Anchor anchor)
        {
            _anchor = anchor;

            for (int i = 0; i < options.Length; i++)
            {
                var dest = GetTextDest(i, KT.Size / 2);
                _options.Add(new Text(options[i], dest, _anchor, Color.White, .6f, 3));
            }
        }

        private Rect GetTextDest(int index, Point pos)
        {
            return _anchor switch
            {
                Anchor.TopLeft => new Rect(pos.X, pos.Y + index * 24, 20, 24),
                Anchor.TopRight => new Rect(pos.X, pos.Y + index * 24, 20, 24),
                Anchor.BottomLeft => new Rect(pos.X, pos.Y - index * 24, 20, 24),
                Anchor.BottomRight => new Rect(pos.X, pos.Y - index * 24, 20, 24),
                _ => throw new Exception($"error: Anchor \"{_anchor}\" isn't supported")
            };
        }

        public void Init()
        {
            foreach (var option in _options)
            {
                option.Init();
                option.Show();
            }
        }

        public void Show()
        {
            foreach (var option in _options)
            {
                option.Show();
            }
        }

        public void Hide()
        {
            foreach (var option in _options)
            {
                option.Hide();
            }
        }
    }
}
