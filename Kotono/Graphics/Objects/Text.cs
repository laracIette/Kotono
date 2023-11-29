using Kotono.Graphics.Objects.Managers;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    public class Text : IObject2D, ISelectable
    {
        protected string _text;

        protected Anchor _anchor;

        protected float _spacing;

        protected readonly List<Image> _letters = new();

        public Rect LettersDest;

        private readonly RoundedBorder _roundedBorder;

        public Rect Dest
        {
            get => new Rect(LettersDest.X, LettersDest.Y, LettersDest.W * _text.Length * _spacing, LettersDest.H);
            set 
            { 
                LettersDest = value;
                /// Don't divide by 0
                //LettersDest.W /= Math.Max(1, _text.Length);

                for (int i = 0; i < _letters.Count; i++)
                {
                    _letters[i].Dest = GetLetterDest(i, value);
                }

                if (_roundedBorder != null)
                {
                    _roundedBorder.Dest = Dest;
                }
            }
        }

        public double StartTime { get; private set; }

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

        public bool IsMouseOn => Rect.Overlaps(Dest, Mouse.Position);

        public static void InitPaths()
        {
            _paths['a'] = Path.Kotono + @"Assets\Characters\a.png"; Texture.Load(_paths['a'], TextureUnit.Texture0);
            _paths['b'] = Path.Kotono + @"Assets\Characters\b.png"; Texture.Load(_paths['b'], TextureUnit.Texture0);
            _paths['c'] = Path.Kotono + @"Assets\Characters\c.png"; Texture.Load(_paths['c'], TextureUnit.Texture0);
            _paths['d'] = Path.Kotono + @"Assets\Characters\d.png"; Texture.Load(_paths['d'], TextureUnit.Texture0);
            _paths['e'] = Path.Kotono + @"Assets\Characters\e.png"; Texture.Load(_paths['e'], TextureUnit.Texture0);
            _paths['f'] = Path.Kotono + @"Assets\Characters\f.png"; Texture.Load(_paths['f'], TextureUnit.Texture0);
            _paths['g'] = Path.Kotono + @"Assets\Characters\g.png"; Texture.Load(_paths['g'], TextureUnit.Texture0);
            _paths['h'] = Path.Kotono + @"Assets\Characters\h.png"; Texture.Load(_paths['h'], TextureUnit.Texture0);
            _paths['i'] = Path.Kotono + @"Assets\Characters\i.png"; Texture.Load(_paths['i'], TextureUnit.Texture0);
            _paths['j'] = Path.Kotono + @"Assets\Characters\j.png"; Texture.Load(_paths['j'], TextureUnit.Texture0);
            _paths['k'] = Path.Kotono + @"Assets\Characters\k.png"; Texture.Load(_paths['k'], TextureUnit.Texture0);
            _paths['l'] = Path.Kotono + @"Assets\Characters\l.png"; Texture.Load(_paths['l'], TextureUnit.Texture0);
            _paths['m'] = Path.Kotono + @"Assets\Characters\m.png"; Texture.Load(_paths['m'], TextureUnit.Texture0);
            _paths['n'] = Path.Kotono + @"Assets\Characters\n.png"; Texture.Load(_paths['n'], TextureUnit.Texture0);
            _paths['o'] = Path.Kotono + @"Assets\Characters\o.png"; Texture.Load(_paths['o'], TextureUnit.Texture0);
            _paths['p'] = Path.Kotono + @"Assets\Characters\p.png"; Texture.Load(_paths['p'], TextureUnit.Texture0);
            _paths['q'] = Path.Kotono + @"Assets\Characters\q.png"; Texture.Load(_paths['q'], TextureUnit.Texture0);
            _paths['r'] = Path.Kotono + @"Assets\Characters\r.png"; Texture.Load(_paths['r'], TextureUnit.Texture0);
            _paths['s'] = Path.Kotono + @"Assets\Characters\s.png"; Texture.Load(_paths['s'], TextureUnit.Texture0);
            _paths['t'] = Path.Kotono + @"Assets\Characters\t.png"; Texture.Load(_paths['t'], TextureUnit.Texture0);
            _paths['u'] = Path.Kotono + @"Assets\Characters\u.png"; Texture.Load(_paths['u'], TextureUnit.Texture0);
            _paths['v'] = Path.Kotono + @"Assets\Characters\v.png"; Texture.Load(_paths['v'], TextureUnit.Texture0);
            _paths['w'] = Path.Kotono + @"Assets\Characters\w.png"; Texture.Load(_paths['w'], TextureUnit.Texture0);
            _paths['x'] = Path.Kotono + @"Assets\Characters\x.png"; Texture.Load(_paths['x'], TextureUnit.Texture0);
            _paths['y'] = Path.Kotono + @"Assets\Characters\y.png"; Texture.Load(_paths['y'], TextureUnit.Texture0);
            _paths['z'] = Path.Kotono + @"Assets\Characters\z.png"; Texture.Load(_paths['z'], TextureUnit.Texture0);
            _paths['A'] = Path.Kotono + @"Assets\Characters\ua.png"; Texture.Load(_paths['A'], TextureUnit.Texture0);
            _paths['B'] = Path.Kotono + @"Assets\Characters\ub.png"; Texture.Load(_paths['B'], TextureUnit.Texture0);
            _paths['C'] = Path.Kotono + @"Assets\Characters\uc.png"; Texture.Load(_paths['C'], TextureUnit.Texture0);
            _paths['D'] = Path.Kotono + @"Assets\Characters\ud.png"; Texture.Load(_paths['D'], TextureUnit.Texture0);
            _paths['E'] = Path.Kotono + @"Assets\Characters\ue.png"; Texture.Load(_paths['E'], TextureUnit.Texture0);
            _paths['F'] = Path.Kotono + @"Assets\Characters\uf.png"; Texture.Load(_paths['F'], TextureUnit.Texture0);
            _paths['G'] = Path.Kotono + @"Assets\Characters\ug.png"; Texture.Load(_paths['G'], TextureUnit.Texture0);
            _paths['H'] = Path.Kotono + @"Assets\Characters\uh.png"; Texture.Load(_paths['H'], TextureUnit.Texture0);
            _paths['I'] = Path.Kotono + @"Assets\Characters\ui.png"; Texture.Load(_paths['I'], TextureUnit.Texture0);
            _paths['J'] = Path.Kotono + @"Assets\Characters\uj.png"; Texture.Load(_paths['J'], TextureUnit.Texture0);
            _paths['K'] = Path.Kotono + @"Assets\Characters\uk.png"; Texture.Load(_paths['K'], TextureUnit.Texture0);
            _paths['L'] = Path.Kotono + @"Assets\Characters\ul.png"; Texture.Load(_paths['L'], TextureUnit.Texture0);
            _paths['M'] = Path.Kotono + @"Assets\Characters\um.png"; Texture.Load(_paths['M'], TextureUnit.Texture0);
            _paths['N'] = Path.Kotono + @"Assets\Characters\un.png"; Texture.Load(_paths['N'], TextureUnit.Texture0);
            _paths['O'] = Path.Kotono + @"Assets\Characters\uo.png"; Texture.Load(_paths['O'], TextureUnit.Texture0);
            _paths['P'] = Path.Kotono + @"Assets\Characters\up.png"; Texture.Load(_paths['P'], TextureUnit.Texture0);
            _paths['Q'] = Path.Kotono + @"Assets\Characters\uq.png"; Texture.Load(_paths['Q'], TextureUnit.Texture0);
            _paths['R'] = Path.Kotono + @"Assets\Characters\ur.png"; Texture.Load(_paths['R'], TextureUnit.Texture0);
            _paths['S'] = Path.Kotono + @"Assets\Characters\us.png"; Texture.Load(_paths['S'], TextureUnit.Texture0);
            _paths['T'] = Path.Kotono + @"Assets\Characters\ut.png"; Texture.Load(_paths['T'], TextureUnit.Texture0);
            _paths['U'] = Path.Kotono + @"Assets\Characters\uu.png"; Texture.Load(_paths['U'], TextureUnit.Texture0);
            _paths['V'] = Path.Kotono + @"Assets\Characters\uv.png"; Texture.Load(_paths['V'], TextureUnit.Texture0);
            _paths['W'] = Path.Kotono + @"Assets\Characters\uw.png"; Texture.Load(_paths['W'], TextureUnit.Texture0);
            _paths['X'] = Path.Kotono + @"Assets\Characters\ux.png"; Texture.Load(_paths['X'], TextureUnit.Texture0);
            _paths['Y'] = Path.Kotono + @"Assets\Characters\uy.png"; Texture.Load(_paths['Y'], TextureUnit.Texture0);
            _paths['Z'] = Path.Kotono + @"Assets\Characters\uz.png"; Texture.Load(_paths['Z'], TextureUnit.Texture0);
            _paths[' '] = Path.Kotono + @"Assets\Characters\space.png"; Texture.Load(_paths[' '], TextureUnit.Texture0);
            _paths['0'] = Path.Kotono + @"Assets\Characters\0.png"; Texture.Load(_paths['0'], TextureUnit.Texture0);
            _paths['1'] = Path.Kotono + @"Assets\Characters\1.png"; Texture.Load(_paths['1'], TextureUnit.Texture0);
            _paths['2'] = Path.Kotono + @"Assets\Characters\2.png"; Texture.Load(_paths['2'], TextureUnit.Texture0);
            _paths['3'] = Path.Kotono + @"Assets\Characters\3.png"; Texture.Load(_paths['3'], TextureUnit.Texture0);
            _paths['4'] = Path.Kotono + @"Assets\Characters\4.png"; Texture.Load(_paths['4'], TextureUnit.Texture0);
            _paths['5'] = Path.Kotono + @"Assets\Characters\5.png"; Texture.Load(_paths['5'], TextureUnit.Texture0);
            _paths['6'] = Path.Kotono + @"Assets\Characters\6.png"; Texture.Load(_paths['6'], TextureUnit.Texture0);
            _paths['7'] = Path.Kotono + @"Assets\Characters\7.png"; Texture.Load(_paths['7'], TextureUnit.Texture0);
            _paths['8'] = Path.Kotono + @"Assets\Characters\8.png"; Texture.Load(_paths['8'], TextureUnit.Texture0);
            _paths['9'] = Path.Kotono + @"Assets\Characters\9.png"; Texture.Load(_paths['9'], TextureUnit.Texture0);
            _paths['!'] = Path.Kotono + @"Assets\Characters\exclamation.png"; Texture.Load(_paths['!'], TextureUnit.Texture0);
            _paths['?'] = Path.Kotono + @"Assets\Characters\interrogation.png"; Texture.Load(_paths['?'], TextureUnit.Texture0);
            _paths['/'] = Path.Kotono + @"Assets\Characters\fslash.png"; Texture.Load(_paths['/'], TextureUnit.Texture0);
            _paths['\\'] = Path.Kotono + @"Assets\Characters\bslash.png"; Texture.Load(_paths['\\'], TextureUnit.Texture0);
            _paths['.'] = Path.Kotono + @"Assets\Characters\dot.png"; Texture.Load(_paths['.'], TextureUnit.Texture0);
            _paths['-'] = Path.Kotono + @"Assets\Characters\minus.png"; Texture.Load(_paths['-'], TextureUnit.Texture0);
            _paths['+'] = Path.Kotono + @"Assets\Characters\plus.png"; Texture.Load(_paths['+'], TextureUnit.Texture0);
            _paths[':'] = Path.Kotono + @"Assets\Characters\colon.png"; Texture.Load(_paths[':'], TextureUnit.Texture0);
            _paths['#'] = Path.Kotono + @"Assets\Characters\#.png"; Texture.Load(_paths['#'], TextureUnit.Texture0);
            _paths['\''] = Path.Kotono + @"Assets\Characters\'.png"; Texture.Load(_paths['\''], TextureUnit.Texture0);
        }

        public Text(string text, Rect lettersDest, Anchor position, Color color, float spacing, int layer)
        {
            _text = text;
            Dest = lettersDest;
            _anchor = position;
            Color = color;
            _spacing = spacing;
            Layer = layer;

            _roundedBorder = new RoundedBorder(Dest, Color.Red, 2, 0, 0, 1);

            ObjectManager.Create(this);
        }

        public void Init()
        {
            Clear();

            StartTime = Time.NowS;

            for (int i = 0; i < _text.Length; i++)
            {
                string path = _paths.TryGetValue(_text[i], out string? tempPath) ? tempPath : _paths[' '];

                var dest = GetLetterDest(i, LettersDest);

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
            if (IsMouseOn)
            {
                KT.Print(_text);
            }
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
            LettersDest += dest;
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

        public void UpdateShaders()
        {

        }

        public void Draw()
        {

        }

        public void Save()
        {

        }

        public override string ToString()
        {
            return _text;
        }

        public void Delete()
        {
            ObjectManager.Delete(this);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
