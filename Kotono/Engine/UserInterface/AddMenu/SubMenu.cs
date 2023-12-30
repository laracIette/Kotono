using Kotono.Graphics.Objects;
using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Engine.UserInterface.AddMenu
{
    public class SubMenu
    {
        private readonly List<Text> _options = [];

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

        public bool IsDraw
        {
            get => _options.FirstOrDefault()?.IsDraw ?? throw new Exception("error: cannot access IsDraw, _frames is empty.");
            set
            {
                foreach (var option in _options)
                {
                    option.IsDraw = value;
                }
            }
        }

        public SubMenu(string[] options, Anchor anchor)
        {
            _anchor = anchor;

            for (int i = 0; i < options.Length; i++)
            {
                var dest = GetTextDest(i, KT.Size / 2.0f);
                _options.Add(new Text(options[i], dest, _anchor, Color.White, 0.6f, 3));
                _options[i].IsDraw = true;
                _options[i].Init();
            }
        }

        private Rect GetTextDest(int index, Point pos)
        {
            return _anchor switch
            {
                Anchor.TopLeft => new Rect(pos.X, pos.Y + index * 24.0f, 20.0f, 24.0f),
                Anchor.TopRight => new Rect(pos.X, pos.Y + index * 24.0f, 20.0f, 24.0f),
                Anchor.BottomLeft => new Rect(pos.X, pos.Y - index * 24.0f, 20.0f, 24.0f),
                Anchor.BottomRight => new Rect(pos.X, pos.Y - index * 24.0f, 20.0f, 24.0f),
                _ => throw new Exception($"error: Anchor \"{_anchor}\" isn't supported")
            };
        }
    }
}
