using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using System.Collections.Generic;

namespace Kotono.Engine.UserInterface.AddMenu
{
    internal class SubMenu
    {
        private readonly List<Text> _options = [];

        private readonly Anchor _anchor;

        internal Point Position
        {
            set
            {
                for (int i = 0; i < _options.Count; i++)
                {
                    _options[i].Position = GetTextRect(i, value).Position;
                }
            }
        }

        internal bool IsDraw
        {
            get => _options.FirstOrNull()?.IsDraw ?? throw new KotonoException("cannot access IsDraw, _frames is empty");
            set
            {
                _options.ForEach(o => o.IsDraw = value);
            }
        }

        internal SubMenu(string[] options, Anchor anchor)
        {
            _anchor = anchor;

            for (int i = 0; i < options.Length; i++)
            {
                var Rect = GetTextRect(i, Window.Size / 2.0f);
                _options.Add(
                    new Text(
                        new TextSettings
                        {
                            Rect = Rect,
                            Layer = 3,
                            Text = options[i],
                            Anchor = _anchor,
                            Spacing = 0.6f
                        }
                    )
                );
            }
        }

        private Rect GetTextRect(int index, Point pos)
        {
            return _anchor switch
            {
                Anchor.TopLeft => new Rect(pos.X, pos.Y + index * 24.0f, 20.0f, 24.0f),
                Anchor.TopRight => new Rect(pos.X, pos.Y + index * 24.0f, 20.0f, 24.0f),
                Anchor.BottomLeft => new Rect(pos.X, pos.Y - index * 24.0f, 20.0f, 24.0f),
                Anchor.BottomRight => new Rect(pos.X, pos.Y - index * 24.0f, 20.0f, 24.0f),
                _ => throw new SwitchException(typeof(Anchor), _anchor)
            };
        }
    }
}
