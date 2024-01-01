using Kotono.Input;
using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects.Texts
{
    public class Text : Object2D, ISelectable
    {
        protected string _text;

        protected Anchor _anchor;

        protected float _spacing;

        protected readonly List<Image> _letters = [];

        protected Rect _lettersDest;

        private readonly RoundedBorder _roundedBorder;

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
            get => _letters.FirstOrDefault()?.IsDraw ?? false; // don't draw if empty
            set
            {
                foreach (var frame in _letters)
                {
                    frame.IsDraw = value;
                }
            }
        }

        public double StartTime { get; private set; }

        private readonly static Dictionary<char, string> _charactersPath = new()
        {
            ['a'] = @"Assets\Characters\a.png",
            ['b'] = @"Assets\Characters\b.png",
            ['c'] = @"Assets\Characters\c.png",
            ['d'] = @"Assets\Characters\d.png",
            ['e'] = @"Assets\Characters\e.png",
            ['f'] = @"Assets\Characters\f.png",
            ['g'] = @"Assets\Characters\g.png",
            ['h'] = @"Assets\Characters\h.png",
            ['i'] = @"Assets\Characters\i.png",
            ['j'] = @"Assets\Characters\j.png",
            ['k'] = @"Assets\Characters\k.png",
            ['l'] = @"Assets\Characters\l.png",
            ['m'] = @"Assets\Characters\m.png",
            ['n'] = @"Assets\Characters\n.png",
            ['o'] = @"Assets\Characters\o.png",
            ['p'] = @"Assets\Characters\p.png",
            ['q'] = @"Assets\Characters\q.png",
            ['r'] = @"Assets\Characters\r.png",
            ['s'] = @"Assets\Characters\s.png",
            ['t'] = @"Assets\Characters\t.png",
            ['u'] = @"Assets\Characters\u.png",
            ['v'] = @"Assets\Characters\v.png",
            ['w'] = @"Assets\Characters\w.png",
            ['x'] = @"Assets\Characters\x.png",
            ['y'] = @"Assets\Characters\y.png",
            ['z'] = @"Assets\Characters\z.png",
            ['A'] = @"Assets\Characters\ua.png",
            ['B'] = @"Assets\Characters\ub.png",
            ['C'] = @"Assets\Characters\uc.png",
            ['D'] = @"Assets\Characters\ud.png",
            ['E'] = @"Assets\Characters\ue.png",
            ['F'] = @"Assets\Characters\uf.png",
            ['G'] = @"Assets\Characters\ug.png",
            ['H'] = @"Assets\Characters\uh.png",
            ['I'] = @"Assets\Characters\ui.png",
            ['J'] = @"Assets\Characters\uj.png",
            ['K'] = @"Assets\Characters\uk.png",
            ['L'] = @"Assets\Characters\ul.png",
            ['M'] = @"Assets\Characters\um.png",
            ['N'] = @"Assets\Characters\un.png",
            ['O'] = @"Assets\Characters\uo.png",
            ['P'] = @"Assets\Characters\up.png",
            ['Q'] = @"Assets\Characters\uq.png",
            ['R'] = @"Assets\Characters\ur.png",
            ['S'] = @"Assets\Characters\us.png",
            ['T'] = @"Assets\Characters\ut.png",
            ['U'] = @"Assets\Characters\uu.png",
            ['V'] = @"Assets\Characters\uv.png",
            ['W'] = @"Assets\Characters\uw.png",
            ['X'] = @"Assets\Characters\ux.png",
            ['Y'] = @"Assets\Characters\uy.png",
            ['Z'] = @"Assets\Characters\uz.png",
            [' '] = @"Assets\Characters\space.png",
            ['0'] = @"Assets\Characters\0.png",
            ['1'] = @"Assets\Characters\1.png",
            ['2'] = @"Assets\Characters\2.png",
            ['3'] = @"Assets\Characters\3.png",
            ['4'] = @"Assets\Characters\4.png",
            ['5'] = @"Assets\Characters\5.png",
            ['6'] = @"Assets\Characters\6.png",
            ['7'] = @"Assets\Characters\7.png",
            ['8'] = @"Assets\Characters\8.png",
            ['9'] = @"Assets\Characters\9.png",
            ['!'] = @"Assets\Characters\exclamation.png",
            ['?'] = @"Assets\Characters\question.png",
            ['/'] = @"Assets\Characters\fslash.png",
            ['\\'] = @"Assets\Characters\bslash.png",
            ['.'] = @"Assets\Characters\dot.png",
            ['-'] = @"Assets\Characters\minus.png",
            ['+'] = @"Assets\Characters\plus.png",
            [':'] = @"Assets\Characters\colon.png",
            ['#'] = @"Assets\Characters\#.png",
            ['\''] = @"Assets\Characters\'.png"
        };

        private Color _color;

        public Color Color
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

        public bool IsSelected { get; } = false;

        public bool IsActive { get; } = false;

        public bool IsMouseOn => Rect.Overlaps(Dest, Mouse.Position);

        public Text(string text, Rect lettersDest, Anchor position, Color color, float spacing, int layer)
            : base()
        {
            _text = text;
            _lettersDest = lettersDest;
            _anchor = position;
            Color = color;
            _spacing = spacing;
            Layer = layer;

            _roundedBorder = new RoundedBorder(Dest, Color.Red, 2, 0.0f, 0.0f, 1.0f)
            {
                IsDraw = false
            };
        }

        public void Init()
        {
            Clear();

            StartTime = Time.NowS;

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
                        Path = Path.Kotono + path,
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

        public virtual void SetText(string text)
        {
            if (IsDraw && text != _text)
            {
                _text = text;
                Init();
            }
        }

        public void Transform(Rect dest)
        {
            _lettersDest += dest;
            foreach (var letter in _letters)
            {
                letter.Transform(dest);
            }
        }

        public void Transform(Rect dest, double time)
        {
            foreach (var letter in _letters)
            {
                letter.Transform(dest, time);
            }
        }

        public void Clear()
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
