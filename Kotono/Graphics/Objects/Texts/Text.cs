using Kotono.Settings;
using Kotono.Input;
using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Texts
{
    internal class Text : Object2D
    {
        private readonly static Dictionary<char, string> _charactersPath = new()
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

        protected string _text;

        protected Anchor _anchor;

        protected float _spacing;

        protected readonly List<Image> _letters = [];

        private readonly RoundedBorder _roundedBorder;

        protected Rect _lettersDest;

        public override Rect Dest
        {
            get => Rect.FromAnchor(new Rect(_lettersDest.X, _lettersDest.Y, _lettersDest.W * _text.Length * _spacing, _lettersDest.H), _anchor);
            set => _lettersDest = value;
        }

        public override Point Position
        {
            get => Dest.Position;
            set
            {
                _lettersDest.Position = value;

                for (int i = 0; i < _letters.Count; i++)
                {
                    _letters[i].Dest = GetLetterDest(i, _lettersDest);
                }

                if (_roundedBorder != null)
                {
                    _roundedBorder.Dest = Dest;
                }
            }
        }

        public override bool IsDraw
        {
            get => _letters.FirstOrDefault()?.IsDraw ?? false; // Don't draw if empty
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

        internal bool IsMouseOn => Rect.Overlaps(Dest, Mouse.Position);

        internal Text(TextSettings settings)
            : base(settings)
        {
            _text = settings.Text;
            _anchor = settings.Anchor;
            Color = settings.Color;
            _spacing = settings.Spacing;

            _roundedBorder = new RoundedBorder(
                new RoundedBorderSettings
                {
                    IsDraw = false,
                    Dest = Dest,
                    Layer = 2
                }
            );

            Init();
        }

        protected void Init()
        {
            Clear();

            for (int i = 0; i < _text.Length; i++)
            {
                if (!_charactersPath.TryGetValue(_text[i], out string? path))
                {
                    path = _charactersPath[' '];
                }

                Rect dest = GetLetterDest(i, _lettersDest);

                _letters.Add(new Image(
                    new ImageSettings
                    {
                        Texture = Path.ASSETS + path,
                        Dest = dest,
                        Color = Color,
                        Layer = Layer
                    }
                ));
            }
        }

        private Rect GetLetterDest(int index, Rect dest)
        {
            return _anchor switch
            {
                Anchor.Center => new Rect(
                    dest.X - dest.W / 2.0f * (_text.Length - 1) * _spacing + dest.W * index * _spacing,
                    dest.Y,
                    dest.Size
                ),
                Anchor.Top => new Rect(
                    dest.X - dest.W / 2.0f * (_text.Length - 1) * _spacing + dest.W * index * _spacing,
                    dest.Y + dest.H / 2.0f,
                    dest.Size
                ),
                Anchor.Bottom => new Rect(
                    dest.X - dest.W / 2.0f * (_text.Length - 1) * _spacing + dest.W * index * _spacing,
                    dest.Y - dest.H / 2.0f,
                    dest.Size
                ),
                Anchor.Left => new Rect(
                    dest.X + (dest.W / 2.0f + dest.W * index) * _spacing,
                    dest.Y,
                    dest.Size
                ),
                Anchor.Right => new Rect(
                    dest.X - dest.W / 2.0f * _spacing - dest.W * (_text.Length - 1 - index) * _spacing,
                    dest.Y,
                    dest.Size
                ),
                Anchor.TopLeft => new Rect(
                    dest.X + (dest.W / 2.0f + dest.W * index) * _spacing,
                    dest.Y + dest.H / 2.0f,
                    dest.Size
                ),
                Anchor.TopRight => new Rect(
                    dest.X - dest.W / 2.0f * _spacing - dest.W * (_text.Length - 1 - index) * _spacing,
                    dest.Y + dest.H / 2.0f,
                    dest.Size
                ),
                Anchor.BottomLeft => new Rect(
                    dest.X + (dest.W / 2.0f + dest.W * index) * _spacing,
                    dest.Y - dest.H / 2.0f,
                    dest.Size
                ),
                Anchor.BottomRight => new Rect(
                    dest.X - dest.W / 2.0f * _spacing - dest.W * (_text.Length - 1 - index) * _spacing,
                    dest.Y - dest.H / 2.0f,
                    dest.Size
                ),
                _ => throw new Exception($"error: Text.Init()'s switch on Anchor doesn't handle \"{_anchor}\""),
            };
        }

        internal virtual void SetText(string text)
        {
            if (IsDraw && text != _text)
            {
                _text = text;
                Init();
            }
        }

        internal void Transform(Rect dest)
        {
            _lettersDest += dest;
            foreach (var letter in _letters)
            {
                letter.Transform(dest);
            }
        }

        internal void Transform(Rect dest, float time)
        {
            foreach (var letter in _letters)
            {
                letter.Transform(dest, time);
            }
        }

        internal void Clear()
        {
            foreach (var letter in _letters)
            {
                letter.Delete();
            }

            _letters.Clear();
        }

        public override string ToString()
        {
            return _text;
        }
    }
}
