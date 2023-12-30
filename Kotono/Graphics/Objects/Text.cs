using Kotono.Graphics.Objects.Managers;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
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

        private readonly static Dictionary<char, string> _paths = [];

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

        public static void InitPaths()
        {
            _paths['a'] = Path.Kotono + @"Assets\Characters\a.png"; new Texture(_paths['a'], TextureUnit.Texture0);
            _paths['b'] = Path.Kotono + @"Assets\Characters\b.png"; new Texture(_paths['b'], TextureUnit.Texture0);
            _paths['c'] = Path.Kotono + @"Assets\Characters\c.png"; new Texture(_paths['c'], TextureUnit.Texture0);
            _paths['d'] = Path.Kotono + @"Assets\Characters\d.png"; new Texture(_paths['d'], TextureUnit.Texture0);
            _paths['e'] = Path.Kotono + @"Assets\Characters\e.png"; new Texture(_paths['e'], TextureUnit.Texture0);
            _paths['f'] = Path.Kotono + @"Assets\Characters\f.png"; new Texture(_paths['f'], TextureUnit.Texture0);
            _paths['g'] = Path.Kotono + @"Assets\Characters\g.png"; new Texture(_paths['g'], TextureUnit.Texture0);
            _paths['h'] = Path.Kotono + @"Assets\Characters\h.png"; new Texture(_paths['h'], TextureUnit.Texture0);
            _paths['i'] = Path.Kotono + @"Assets\Characters\i.png"; new Texture(_paths['i'], TextureUnit.Texture0);
            _paths['j'] = Path.Kotono + @"Assets\Characters\j.png"; new Texture(_paths['j'], TextureUnit.Texture0);
            _paths['k'] = Path.Kotono + @"Assets\Characters\k.png"; new Texture(_paths['k'], TextureUnit.Texture0);
            _paths['l'] = Path.Kotono + @"Assets\Characters\l.png"; new Texture(_paths['l'], TextureUnit.Texture0);
            _paths['m'] = Path.Kotono + @"Assets\Characters\m.png"; new Texture(_paths['m'], TextureUnit.Texture0);
            _paths['n'] = Path.Kotono + @"Assets\Characters\n.png"; new Texture(_paths['n'], TextureUnit.Texture0);
            _paths['o'] = Path.Kotono + @"Assets\Characters\o.png"; new Texture(_paths['o'], TextureUnit.Texture0);
            _paths['p'] = Path.Kotono + @"Assets\Characters\p.png"; new Texture(_paths['p'], TextureUnit.Texture0);
            _paths['q'] = Path.Kotono + @"Assets\Characters\q.png"; new Texture(_paths['q'], TextureUnit.Texture0);
            _paths['r'] = Path.Kotono + @"Assets\Characters\r.png"; new Texture(_paths['r'], TextureUnit.Texture0);
            _paths['s'] = Path.Kotono + @"Assets\Characters\s.png"; new Texture(_paths['s'], TextureUnit.Texture0);
            _paths['t'] = Path.Kotono + @"Assets\Characters\t.png"; new Texture(_paths['t'], TextureUnit.Texture0);
            _paths['u'] = Path.Kotono + @"Assets\Characters\u.png"; new Texture(_paths['u'], TextureUnit.Texture0);
            _paths['v'] = Path.Kotono + @"Assets\Characters\v.png"; new Texture(_paths['v'], TextureUnit.Texture0);
            _paths['w'] = Path.Kotono + @"Assets\Characters\w.png"; new Texture(_paths['w'], TextureUnit.Texture0);
            _paths['x'] = Path.Kotono + @"Assets\Characters\x.png"; new Texture(_paths['x'], TextureUnit.Texture0);
            _paths['y'] = Path.Kotono + @"Assets\Characters\y.png"; new Texture(_paths['y'], TextureUnit.Texture0);
            _paths['z'] = Path.Kotono + @"Assets\Characters\z.png"; new Texture(_paths['z'], TextureUnit.Texture0);
            _paths['A'] = Path.Kotono + @"Assets\Characters\ua.png"; new Texture(_paths['A'], TextureUnit.Texture0);
            _paths['B'] = Path.Kotono + @"Assets\Characters\ub.png"; new Texture(_paths['B'], TextureUnit.Texture0);
            _paths['C'] = Path.Kotono + @"Assets\Characters\uc.png"; new Texture(_paths['C'], TextureUnit.Texture0);
            _paths['D'] = Path.Kotono + @"Assets\Characters\ud.png"; new Texture(_paths['D'], TextureUnit.Texture0);
            _paths['E'] = Path.Kotono + @"Assets\Characters\ue.png"; new Texture(_paths['E'], TextureUnit.Texture0);
            _paths['F'] = Path.Kotono + @"Assets\Characters\uf.png"; new Texture(_paths['F'], TextureUnit.Texture0);
            _paths['G'] = Path.Kotono + @"Assets\Characters\ug.png"; new Texture(_paths['G'], TextureUnit.Texture0);
            _paths['H'] = Path.Kotono + @"Assets\Characters\uh.png"; new Texture(_paths['H'], TextureUnit.Texture0);
            _paths['I'] = Path.Kotono + @"Assets\Characters\ui.png"; new Texture(_paths['I'], TextureUnit.Texture0);
            _paths['J'] = Path.Kotono + @"Assets\Characters\uj.png"; new Texture(_paths['J'], TextureUnit.Texture0);
            _paths['K'] = Path.Kotono + @"Assets\Characters\uk.png"; new Texture(_paths['K'], TextureUnit.Texture0);
            _paths['L'] = Path.Kotono + @"Assets\Characters\ul.png"; new Texture(_paths['L'], TextureUnit.Texture0);
            _paths['M'] = Path.Kotono + @"Assets\Characters\um.png"; new Texture(_paths['M'], TextureUnit.Texture0);
            _paths['N'] = Path.Kotono + @"Assets\Characters\un.png"; new Texture(_paths['N'], TextureUnit.Texture0);
            _paths['O'] = Path.Kotono + @"Assets\Characters\uo.png"; new Texture(_paths['O'], TextureUnit.Texture0);
            _paths['P'] = Path.Kotono + @"Assets\Characters\up.png"; new Texture(_paths['P'], TextureUnit.Texture0);
            _paths['Q'] = Path.Kotono + @"Assets\Characters\uq.png"; new Texture(_paths['Q'], TextureUnit.Texture0);
            _paths['R'] = Path.Kotono + @"Assets\Characters\ur.png"; new Texture(_paths['R'], TextureUnit.Texture0);
            _paths['S'] = Path.Kotono + @"Assets\Characters\us.png"; new Texture(_paths['S'], TextureUnit.Texture0);
            _paths['T'] = Path.Kotono + @"Assets\Characters\ut.png"; new Texture(_paths['T'], TextureUnit.Texture0);
            _paths['U'] = Path.Kotono + @"Assets\Characters\uu.png"; new Texture(_paths['U'], TextureUnit.Texture0);
            _paths['V'] = Path.Kotono + @"Assets\Characters\uv.png"; new Texture(_paths['V'], TextureUnit.Texture0);
            _paths['W'] = Path.Kotono + @"Assets\Characters\uw.png"; new Texture(_paths['W'], TextureUnit.Texture0);
            _paths['X'] = Path.Kotono + @"Assets\Characters\ux.png"; new Texture(_paths['X'], TextureUnit.Texture0);
            _paths['Y'] = Path.Kotono + @"Assets\Characters\uy.png"; new Texture(_paths['Y'], TextureUnit.Texture0);
            _paths['Z'] = Path.Kotono + @"Assets\Characters\uz.png"; new Texture(_paths['Z'], TextureUnit.Texture0);
            _paths[' '] = Path.Kotono + @"Assets\Characters\space.png"; new Texture(_paths[' '], TextureUnit.Texture0);
            _paths['0'] = Path.Kotono + @"Assets\Characters\0.png"; new Texture(_paths['0'], TextureUnit.Texture0);
            _paths['1'] = Path.Kotono + @"Assets\Characters\1.png"; new Texture(_paths['1'], TextureUnit.Texture0);
            _paths['2'] = Path.Kotono + @"Assets\Characters\2.png"; new Texture(_paths['2'], TextureUnit.Texture0);
            _paths['3'] = Path.Kotono + @"Assets\Characters\3.png"; new Texture(_paths['3'], TextureUnit.Texture0);
            _paths['4'] = Path.Kotono + @"Assets\Characters\4.png"; new Texture(_paths['4'], TextureUnit.Texture0);
            _paths['5'] = Path.Kotono + @"Assets\Characters\5.png"; new Texture(_paths['5'], TextureUnit.Texture0);
            _paths['6'] = Path.Kotono + @"Assets\Characters\6.png"; new Texture(_paths['6'], TextureUnit.Texture0);
            _paths['7'] = Path.Kotono + @"Assets\Characters\7.png"; new Texture(_paths['7'], TextureUnit.Texture0);
            _paths['8'] = Path.Kotono + @"Assets\Characters\8.png"; new Texture(_paths['8'], TextureUnit.Texture0);
            _paths['9'] = Path.Kotono + @"Assets\Characters\9.png"; new Texture(_paths['9'], TextureUnit.Texture0);
            _paths['!'] = Path.Kotono + @"Assets\Characters\exclamation.png"; new Texture(_paths['!'], TextureUnit.Texture0);
            _paths['?'] = Path.Kotono + @"Assets\Characters\interrogation.png"; new Texture(_paths['?'], TextureUnit.Texture0);
            _paths['/'] = Path.Kotono + @"Assets\Characters\fslash.png"; new Texture(_paths['/'], TextureUnit.Texture0);
            _paths['\\'] = Path.Kotono + @"Assets\Characters\bslash.png"; new Texture(_paths['\\'], TextureUnit.Texture0);
            _paths['.'] = Path.Kotono + @"Assets\Characters\dot.png"; new Texture(_paths['.'], TextureUnit.Texture0);
            _paths['-'] = Path.Kotono + @"Assets\Characters\minus.png"; new Texture(_paths['-'], TextureUnit.Texture0);
            _paths['+'] = Path.Kotono + @"Assets\Characters\plus.png"; new Texture(_paths['+'], TextureUnit.Texture0);
            _paths[':'] = Path.Kotono + @"Assets\Characters\colon.png"; new Texture(_paths[':'], TextureUnit.Texture0);
            _paths['#'] = Path.Kotono + @"Assets\Characters\#.png"; new Texture(_paths['#'], TextureUnit.Texture0);
            _paths['\''] = Path.Kotono + @"Assets\Characters\'.png"; new Texture(_paths['\''], TextureUnit.Texture0);
        }

        public Text(string text, Rect lettersDest, Anchor position, Color color, float spacing, int layer)
            : base()
        {
            _text = text;
            _lettersDest = lettersDest;
            _anchor = position;
            Color = color;
            _spacing = spacing;
            Layer = layer;

            _roundedBorder = new RoundedBorder(Dest, Color.Red, 2, 0.0f, 0.0f, 1.0f);
            _roundedBorder.IsDraw = false;
        }

        public void Init()
        {
            Clear();

            StartTime = Time.NowS;

            for (int i = 0; i < _text.Length; i++)
            {
                if (!_paths.TryGetValue(_text[i], out string? path))
                {
                    path = _paths[' '];
                }
                //string path = _paths[_text[i]] ?? _paths[' '];
                Rect dest = GetLetterDest(i, _lettersDest);

                _letters.Add(new Image(
                    new ImageSettings
                    {
                        Path = path,
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
            if (IsDraw && (text != _text))
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
