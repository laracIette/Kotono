﻿using Kotono.Graphics.Objects.Managers;
using Kotono.Input;
using Kotono.Utils;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    public class Text : ISelectable
    {
        protected string _text;

        protected Rect _lettersDest;

        protected Anchor _anchor;

        protected float _spacing;

        protected readonly List<Image> _letters = new();

        public Rect Dest => Rect.FromAnchor(
            new Rect(_lettersDest.X, _lettersDest.Y, _lettersDest.W * _text.Length, _lettersDest.H),
            Anchor.TopLeft
        );

        public Rect LettersDest => _lettersDest;

        public double Time { get; private set; }

        private readonly static Dictionary<char, string> _paths = new();

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

        public bool IsDraw { get; private set; } = true;

        public int Layer { get; set; }

        public bool IsSelected { get; }

        public bool IsActive { get; }

        public static void InitPaths()
        {
            _paths['a'] = Path.Kotono + @"Assets\Characters\a.png"; TextureManager.LoadTexture(_paths['a']);
            _paths['b'] = Path.Kotono + @"Assets\Characters\b.png"; TextureManager.LoadTexture(_paths['b']);
            _paths['c'] = Path.Kotono + @"Assets\Characters\c.png"; TextureManager.LoadTexture(_paths['c']);
            _paths['d'] = Path.Kotono + @"Assets\Characters\d.png"; TextureManager.LoadTexture(_paths['d']);
            _paths['e'] = Path.Kotono + @"Assets\Characters\e.png"; TextureManager.LoadTexture(_paths['e']);
            _paths['f'] = Path.Kotono + @"Assets\Characters\f.png"; TextureManager.LoadTexture(_paths['f']);
            _paths['g'] = Path.Kotono + @"Assets\Characters\g.png"; TextureManager.LoadTexture(_paths['g']);
            _paths['h'] = Path.Kotono + @"Assets\Characters\h.png"; TextureManager.LoadTexture(_paths['h']);
            _paths['i'] = Path.Kotono + @"Assets\Characters\i.png"; TextureManager.LoadTexture(_paths['i']);
            _paths['j'] = Path.Kotono + @"Assets\Characters\j.png"; TextureManager.LoadTexture(_paths['j']);
            _paths['k'] = Path.Kotono + @"Assets\Characters\k.png"; TextureManager.LoadTexture(_paths['k']);
            _paths['l'] = Path.Kotono + @"Assets\Characters\l.png"; TextureManager.LoadTexture(_paths['l']);
            _paths['m'] = Path.Kotono + @"Assets\Characters\m.png"; TextureManager.LoadTexture(_paths['m']);
            _paths['n'] = Path.Kotono + @"Assets\Characters\n.png"; TextureManager.LoadTexture(_paths['n']);
            _paths['o'] = Path.Kotono + @"Assets\Characters\o.png"; TextureManager.LoadTexture(_paths['o']);
            _paths['p'] = Path.Kotono + @"Assets\Characters\p.png"; TextureManager.LoadTexture(_paths['p']);
            _paths['q'] = Path.Kotono + @"Assets\Characters\q.png"; TextureManager.LoadTexture(_paths['q']);
            _paths['r'] = Path.Kotono + @"Assets\Characters\r.png"; TextureManager.LoadTexture(_paths['r']);
            _paths['s'] = Path.Kotono + @"Assets\Characters\s.png"; TextureManager.LoadTexture(_paths['s']);
            _paths['t'] = Path.Kotono + @"Assets\Characters\t.png"; TextureManager.LoadTexture(_paths['t']);
            _paths['u'] = Path.Kotono + @"Assets\Characters\u.png"; TextureManager.LoadTexture(_paths['u']);
            _paths['v'] = Path.Kotono + @"Assets\Characters\v.png"; TextureManager.LoadTexture(_paths['v']);
            _paths['w'] = Path.Kotono + @"Assets\Characters\w.png"; TextureManager.LoadTexture(_paths['w']);
            _paths['x'] = Path.Kotono + @"Assets\Characters\x.png"; TextureManager.LoadTexture(_paths['x']);
            _paths['y'] = Path.Kotono + @"Assets\Characters\y.png"; TextureManager.LoadTexture(_paths['y']);
            _paths['z'] = Path.Kotono + @"Assets\Characters\z.png"; TextureManager.LoadTexture(_paths['z']);
            _paths['A'] = Path.Kotono + @"Assets\Characters\ua.png"; TextureManager.LoadTexture(_paths['A']);
            _paths['B'] = Path.Kotono + @"Assets\Characters\ub.png"; TextureManager.LoadTexture(_paths['B']);
            _paths['C'] = Path.Kotono + @"Assets\Characters\uc.png"; TextureManager.LoadTexture(_paths['C']);
            _paths['D'] = Path.Kotono + @"Assets\Characters\ud.png"; TextureManager.LoadTexture(_paths['D']);
            _paths['E'] = Path.Kotono + @"Assets\Characters\ue.png"; TextureManager.LoadTexture(_paths['E']);
            _paths['F'] = Path.Kotono + @"Assets\Characters\uf.png"; TextureManager.LoadTexture(_paths['F']);
            _paths['G'] = Path.Kotono + @"Assets\Characters\ug.png"; TextureManager.LoadTexture(_paths['G']);
            _paths['H'] = Path.Kotono + @"Assets\Characters\uh.png"; TextureManager.LoadTexture(_paths['H']);
            _paths['I'] = Path.Kotono + @"Assets\Characters\ui.png"; TextureManager.LoadTexture(_paths['I']);
            _paths['J'] = Path.Kotono + @"Assets\Characters\uj.png"; TextureManager.LoadTexture(_paths['J']);
            _paths['K'] = Path.Kotono + @"Assets\Characters\uk.png"; TextureManager.LoadTexture(_paths['K']);
            _paths['L'] = Path.Kotono + @"Assets\Characters\ul.png"; TextureManager.LoadTexture(_paths['L']);
            _paths['M'] = Path.Kotono + @"Assets\Characters\um.png"; TextureManager.LoadTexture(_paths['M']);
            _paths['N'] = Path.Kotono + @"Assets\Characters\un.png"; TextureManager.LoadTexture(_paths['N']);
            _paths['O'] = Path.Kotono + @"Assets\Characters\uo.png"; TextureManager.LoadTexture(_paths['O']);
            _paths['P'] = Path.Kotono + @"Assets\Characters\up.png"; TextureManager.LoadTexture(_paths['P']);
            _paths['Q'] = Path.Kotono + @"Assets\Characters\uq.png"; TextureManager.LoadTexture(_paths['Q']);
            _paths['R'] = Path.Kotono + @"Assets\Characters\ur.png"; TextureManager.LoadTexture(_paths['R']);
            _paths['S'] = Path.Kotono + @"Assets\Characters\us.png"; TextureManager.LoadTexture(_paths['S']);
            _paths['T'] = Path.Kotono + @"Assets\Characters\ut.png"; TextureManager.LoadTexture(_paths['T']);
            _paths['U'] = Path.Kotono + @"Assets\Characters\uu.png"; TextureManager.LoadTexture(_paths['U']);
            _paths['V'] = Path.Kotono + @"Assets\Characters\uv.png"; TextureManager.LoadTexture(_paths['V']);
            _paths['W'] = Path.Kotono + @"Assets\Characters\uw.png"; TextureManager.LoadTexture(_paths['W']);
            _paths['X'] = Path.Kotono + @"Assets\Characters\ux.png"; TextureManager.LoadTexture(_paths['X']);
            _paths['Y'] = Path.Kotono + @"Assets\Characters\uy.png"; TextureManager.LoadTexture(_paths['Y']);
            _paths['Z'] = Path.Kotono + @"Assets\Characters\uz.png"; TextureManager.LoadTexture(_paths['Z']);
            _paths[' '] = Path.Kotono + @"Assets\Characters\space.png"; TextureManager.LoadTexture(_paths[' ']);
            _paths['0'] = Path.Kotono + @"Assets\Characters\0.png"; TextureManager.LoadTexture(_paths['0']);
            _paths['1'] = Path.Kotono + @"Assets\Characters\1.png"; TextureManager.LoadTexture(_paths['1']);
            _paths['2'] = Path.Kotono + @"Assets\Characters\2.png"; TextureManager.LoadTexture(_paths['2']);
            _paths['3'] = Path.Kotono + @"Assets\Characters\3.png"; TextureManager.LoadTexture(_paths['3']);
            _paths['4'] = Path.Kotono + @"Assets\Characters\4.png"; TextureManager.LoadTexture(_paths['4']);
            _paths['5'] = Path.Kotono + @"Assets\Characters\5.png"; TextureManager.LoadTexture(_paths['5']);
            _paths['6'] = Path.Kotono + @"Assets\Characters\6.png"; TextureManager.LoadTexture(_paths['6']);
            _paths['7'] = Path.Kotono + @"Assets\Characters\7.png"; TextureManager.LoadTexture(_paths['7']);
            _paths['8'] = Path.Kotono + @"Assets\Characters\8.png"; TextureManager.LoadTexture(_paths['8']);
            _paths['9'] = Path.Kotono + @"Assets\Characters\9.png"; TextureManager.LoadTexture(_paths['9']);
            _paths['!'] = Path.Kotono + @"Assets\Characters\exclamation.png"; TextureManager.LoadTexture(_paths['!']);
            _paths['?'] = Path.Kotono + @"Assets\Characters\interrogation.png"; TextureManager.LoadTexture(_paths['?']);
            _paths['/'] = Path.Kotono + @"Assets\Characters\fslash.png"; TextureManager.LoadTexture(_paths['/']);
            _paths['\\'] = Path.Kotono + @"Assets\Characters\bslash.png"; TextureManager.LoadTexture(_paths['\\']);
            _paths['.'] = Path.Kotono + @"Assets\Characters\dot.png"; TextureManager.LoadTexture(_paths['.']);
            _paths['-'] = Path.Kotono + @"Assets\Characters\minus.png"; TextureManager.LoadTexture(_paths['-']);
            _paths['+'] = Path.Kotono + @"Assets\Characters\plus.png"; TextureManager.LoadTexture(_paths['+']);
            _paths[':'] = Path.Kotono + @"Assets\Characters\colon.png"; TextureManager.LoadTexture(_paths[':']);
            _paths['#'] = Path.Kotono + @"Assets\Characters\#.png"; TextureManager.LoadTexture(_paths['#']);
            _paths['\''] = Path.Kotono + @"Assets\Characters\'.png"; TextureManager.LoadTexture(_paths['\'']);
        }

        public Text(string text, Rect lettersDest, Anchor position, Color color, float spacing, int layer)
        {
            _text = text;
            _lettersDest = lettersDest;
            _anchor = position;
            Color = color;
            _spacing = spacing;
            Layer = layer;
        }

        public void Init()
        {
            Time = Utils.Time.NowS;

            for (int i = 0; i < _text.Length; i++)
            {
                if (!_paths.TryGetValue(_text[i], out string? path))
                {
                    path = _paths[' '];
                }

                var dest = GetLetterDest(i, _lettersDest);

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
                    dest.X - dest.W / 2 * (_text.Length - 1) * _spacing + dest.W * index * _spacing,
                    dest.Y,
                    dest.Size
                ),
                Anchor.Top => new Rect(
                    dest.X - dest.W / 2 * (_text.Length - 1) * _spacing + dest.W * index * _spacing,
                    dest.Y + dest.H / 2,
                    dest.Size
                ),
                Anchor.Bottom => new Rect(
                    dest.X - dest.W / 2 * (_text.Length - 1) * _spacing + dest.W * index * _spacing,
                    dest.Y - dest.H / 2,
                    dest.Size
                ),
                Anchor.Left => new Rect(
                    dest.X + dest.W / 2 + dest.W * index * _spacing,
                    dest.Y,
                    dest.Size
                ),
                Anchor.Right => new Rect(
                    dest.X - dest.W / 2 - dest.W * (_text.Length - 1 - index) * _spacing,
                    dest.Y,
                    dest.Size
                ),
                Anchor.TopLeft => new Rect(
                    dest.X + dest.W / 2 + dest.W * index * _spacing,
                    dest.Y + dest.H / 2,
                    dest.Size
                ),
                Anchor.TopRight => new Rect(
                    dest.X - dest.W / 2 - dest.W * (_text.Length - 1 - index) * _spacing,
                    dest.Y + dest.H / 2,
                    dest.Size
                ),
                Anchor.BottomLeft => new Rect(
                    dest.X + dest.W / 2 + dest.W * index * _spacing,
                    dest.Y - dest.H / 2,
                    dest.Size
                ),
                Anchor.BottomRight => new Rect(
                    dest.X - dest.W / 2 - dest.W * (_text.Length - 1 - index) * _spacing,
                    dest.Y - dest.H / 2,
                    dest.Size
                ),
                _ => throw new Exception($"error: Text.Init()'s switch on Anchor doesn't handle \"{_anchor}\""),
            };
        }

        public void Update()
        {

        }

        public virtual void SetText(string text)
        {
            if (IsDraw && (text != _text))
            {
                _text = text;
                Clear();
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

        public void TransformTo(Rect dest)
        {
            for (int i = 0; i < _letters.Count; i++)
            {
                _letters[i].TransformTo(GetLetterDest(i, dest));
            }
        }

        public void TransformTo(Rect dest, double time)
        {
            for (int i = 0; i < _letters.Count; i++)
            {
                _letters[i].TransformTo(GetLetterDest(i, dest), time);
            }
        }

        public void Clear()
        {
            foreach (var letter in _letters)
            {
                ObjectManager.Delete(letter);
            }
            _letters.Clear();
        }

        public bool IsMouseOn()
        {
            return Rect.Overlaps(Dest, Mouse.Position);
        }

        public void Show()
        {
            IsDraw = true;
            foreach (var letter in _letters)
            {
                letter.Show();
            }
        }

        public void Hide()
        {
            IsDraw = false;
            foreach (var letter in _letters)
            {
                letter.Hide();
            }
        }
    }
}
