using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Texts
{
    internal class Text : Object2D<TextSettings>
    {
        private static readonly Dictionary<char, string> _charactersPath = new()
        {
            ['a'] = @"Characters\a.png",
            ['b'] = @"Characters\b.png",
            ['c'] = @"Characters\c.png",
            ['d'] = @"Characters\d.png",
            ['e'] = @"Characters\e.png",
            ['f'] = @"Characters\f.png",
            ['g'] = @"Characters\g.png",
            ['h'] = @"Characters\h.png",
            ['i'] = @"Characters\i.png",
            ['j'] = @"Characters\j.png",
            ['k'] = @"Characters\k.png",
            ['l'] = @"Characters\l.png",
            ['m'] = @"Characters\m.png",
            ['n'] = @"Characters\n.png",
            ['o'] = @"Characters\o.png",
            ['p'] = @"Characters\p.png",
            ['q'] = @"Characters\q.png",
            ['r'] = @"Characters\r.png",
            ['s'] = @"Characters\s.png",
            ['t'] = @"Characters\t.png",
            ['u'] = @"Characters\u.png",
            ['v'] = @"Characters\v.png",
            ['w'] = @"Characters\w.png",
            ['x'] = @"Characters\x.png",
            ['y'] = @"Characters\y.png",
            ['z'] = @"Characters\z.png",
            ['A'] = @"Characters\ua.png",
            ['B'] = @"Characters\ub.png",
            ['C'] = @"Characters\uc.png",
            ['D'] = @"Characters\ud.png",
            ['E'] = @"Characters\ue.png",
            ['F'] = @"Characters\uf.png",
            ['G'] = @"Characters\ug.png",
            ['H'] = @"Characters\uh.png",
            ['I'] = @"Characters\ui.png",
            ['J'] = @"Characters\uj.png",
            ['K'] = @"Characters\uk.png",
            ['L'] = @"Characters\ul.png",
            ['M'] = @"Characters\um.png",
            ['N'] = @"Characters\un.png",
            ['O'] = @"Characters\uo.png",
            ['P'] = @"Characters\up.png",
            ['Q'] = @"Characters\uq.png",
            ['R'] = @"Characters\ur.png",
            ['S'] = @"Characters\us.png",
            ['T'] = @"Characters\ut.png",
            ['U'] = @"Characters\uu.png",
            ['V'] = @"Characters\uv.png",
            ['W'] = @"Characters\uw.png",
            ['X'] = @"Characters\ux.png",
            ['Y'] = @"Characters\uy.png",
            ['Z'] = @"Characters\uz.png",
            [' '] = @"Characters\space.png",
            ['0'] = @"Characters\0.png",
            ['1'] = @"Characters\1.png",
            ['2'] = @"Characters\2.png",
            ['3'] = @"Characters\3.png",
            ['4'] = @"Characters\4.png",
            ['5'] = @"Characters\5.png",
            ['6'] = @"Characters\6.png",
            ['7'] = @"Characters\7.png",
            ['8'] = @"Characters\8.png",
            ['9'] = @"Characters\9.png",
            ['!'] = @"Characters\exclamation.png",
            ['?'] = @"Characters\question.png",
            ['/'] = @"Characters\fslash.png",
            ['\\'] = @"Characters\bslash.png",
            ['.'] = @"Characters\dot.png",
            ['-'] = @"Characters\minus.png",
            ['+'] = @"Characters\plus.png",
            [':'] = @"Characters\colon.png",
            ['#'] = @"Characters\#.png",
            ['\''] = @"Characters\'.png",
            ['['] = @"Characters\[.png",
            [']'] = @"Characters\].png",
            [','] = @"Characters\,.png"
        };

        internal string Value { get; private set; } = "";

        internal Anchor Anchor { get; private set; } = Anchor.Center;

        internal float Spacing { get; private set; } = 1.0f;

        protected readonly List<Image> _letters = [];

        private readonly RoundedBorder _roundedBorder;

        protected Rect _lettersRect = Rect.Default; // welp

        private readonly Rect _rect = Rect.Default; // welp

        private object? _source = null;

        internal virtual object? Source 
        { 
            get => _source;
            set
            {
                var newValue = value?.ToString() ?? "";

                if (Value != newValue)
                {
                    Value = newValue;
                    Init();
                }

                _source = value;
            } 
        }

        public override Rect Rect
        {
            get
            {
                _rect.Position = Rect.FromAnchor(_lettersRect.Position, new Point(_lettersRect.BaseSize.X * Value.Length * Spacing, _lettersRect.BaseSize.Y), Anchor);
                _rect.BaseSize = new Point(_lettersRect.BaseSize.X * Value.Length * Spacing, _lettersRect.BaseSize.Y);
                return _rect;
            }
            set => _lettersRect = value;
        }

        public override Point Position
        {
            get => Rect.Position;
            set
            {
                _lettersRect.Position = value;

                for (int i = 0; i < _letters.Count; i++)
                {
                    _letters[i].Rect.Position = GetLetterPosition(i, _lettersRect.Position, _lettersRect.Size);
                }

                if (_roundedBorder != null)
                {
                    _roundedBorder.Rect = Rect;
                }
            }
        }

        public override bool IsDraw
        {
            get => _letters.FirstOrNull()?.IsDraw ?? false; // Don't draw if empty
            set
            {
                foreach (var frame in _letters)
                {
                    frame.IsDraw = value;
                }
            }
        }

        private Color _color;

        public override Color Color
        {
            get => _color;
            set
            {
                _color = value;
                foreach (var letter in _letters)
                {
                    letter.Color = value;
                }
            }
        }

        public new bool IsSelected { get; } = false;

        public new bool IsActive { get; } = false;

        internal bool IsMouseOn => Rect.Overlaps(Rect, Mouse.Position);

        internal Text(TextSettings settings)
            : base(settings)
        {
            Source = settings.Source; 
            Anchor = settings.Anchor;
            Color = settings.Color;
            Spacing = settings.Spacing;

            _roundedBorder = new RoundedBorder(
                new RoundedBorderSettings
                {
                    IsDraw = false,
                    Rect = Rect,
                    Layer = 2
                }
            );

            Init();
        }

        protected void Init()
        {
            Clear();

            for (int i = 0; i < Value.Length; i++)
            {
                if (!_charactersPath.TryGetValue(Value[i], out string? path))
                {
                    path = _charactersPath[' '];
                }

                var position = GetLetterPosition(i, _lettersRect.Position, _lettersRect.Size);

                _letters.Add(new Image(
                    new ImageSettings
                    {
                        Texture = Path.ASSETS + path,
                        Rect = new Rect(position, _lettersRect.Size),
                        Color = Color,
                        Layer = Layer
                    }
                ));
            }
        }

        private Point GetLetterPosition(int index, Point position, Point size)
        {
            position = Anchor switch
            {
                Anchor.Center => new Point(
                    position.X + Spacing * size.X * (index - (Value.Length - 1) / 2.0f),
                    position.Y
                ),
                Anchor.Top => new Point(
                    position.X + Spacing * size.X * (index - (Value.Length - 1) / 2.0f),
                    position.Y + size.Y / 2.0f
                ),
                Anchor.Bottom => new Point(
                    position.X + Spacing * size.X * (index - (Value.Length - 1) / 2.0f),
                    position.Y - size.Y / 2.0f
                ),
                Anchor.Left => new Point(
                    position.X + Spacing * size.X * (0.5f + index),
                    position.Y
                ),
                Anchor.Right => new Point(
                    position.X - Spacing * size.X * (1.5f + index - Value.Length),
                    position.Y
                ),
                Anchor.TopLeft => new Point(
                    position.X + Spacing * size.X * (0.5f + index),
                    position.Y + size.Y / 2.0f
                ),
                Anchor.TopRight => new Point(
                    position.X - Spacing * size.X * (1.5f + index - Value.Length),
                    position.Y + size.Y / 2.0f
                ),
                Anchor.BottomLeft => new Point(
                    position.X + Spacing * size.X * (0.5f + index),
                    position.Y - size.Y / 2.0f
                ),
                Anchor.BottomRight => new Point(
                    position.X - Spacing * size.X * (1.5f + index - Value.Length),
                    position.Y - size.Y / 2.0f
                ),
                _ => throw new Exception($"error: Text.Init()'s switch on Anchor doesn't handle \"{Anchor}\""),
            };

            return position;
        }

        internal void Clear()
        {
            foreach (var letter in _letters)
            {
                letter.Dispose();
            }

            _letters.Clear();
        }

        public override void Dispose()
        {
            _lettersRect.Dispose();

            base.Dispose();
        }

        public override string ToString() => Value;
    }
}
