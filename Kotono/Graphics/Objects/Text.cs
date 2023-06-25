using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    public enum Location
    {
        Center,
        TopLeft,
    }

    internal class Text
    {
        protected string _text;

        protected Rect _dest;

        protected Location _location;

        protected float _spacing;

        protected readonly List<Image> _letters = new();

        internal double Time { get; private set; }

        private readonly static Dictionary<char, string> _paths = new();

        internal static void InitPaths()
        {
            _paths['a'] = KT.KotonoPath + @"Assets\Characters\a.png"; TextureManager.LoadTexture(_paths['a']);
            _paths['b'] = KT.KotonoPath + @"Assets\Characters\b.png"; TextureManager.LoadTexture(_paths['b']);
            _paths['c'] = KT.KotonoPath + @"Assets\Characters\c.png"; TextureManager.LoadTexture(_paths['c']);
            _paths['d'] = KT.KotonoPath + @"Assets\Characters\d.png"; TextureManager.LoadTexture(_paths['d']);
            _paths['e'] = KT.KotonoPath + @"Assets\Characters\e.png"; TextureManager.LoadTexture(_paths['e']);
            _paths['f'] = KT.KotonoPath + @"Assets\Characters\f.png"; TextureManager.LoadTexture(_paths['f']);
            _paths['g'] = KT.KotonoPath + @"Assets\Characters\g.png"; TextureManager.LoadTexture(_paths['g']);
            _paths['h'] = KT.KotonoPath + @"Assets\Characters\h.png"; TextureManager.LoadTexture(_paths['h']);
            _paths['i'] = KT.KotonoPath + @"Assets\Characters\i.png"; TextureManager.LoadTexture(_paths['i']);
            _paths['j'] = KT.KotonoPath + @"Assets\Characters\j.png"; TextureManager.LoadTexture(_paths['j']);
            _paths['k'] = KT.KotonoPath + @"Assets\Characters\k.png"; TextureManager.LoadTexture(_paths['k']);
            _paths['l'] = KT.KotonoPath + @"Assets\Characters\l.png"; TextureManager.LoadTexture(_paths['l']);
            _paths['m'] = KT.KotonoPath + @"Assets\Characters\m.png"; TextureManager.LoadTexture(_paths['m']);
            _paths['n'] = KT.KotonoPath + @"Assets\Characters\n.png"; TextureManager.LoadTexture(_paths['n']);
            _paths['o'] = KT.KotonoPath + @"Assets\Characters\o.png"; TextureManager.LoadTexture(_paths['o']);
            _paths['p'] = KT.KotonoPath + @"Assets\Characters\p.png"; TextureManager.LoadTexture(_paths['p']);
            _paths['q'] = KT.KotonoPath + @"Assets\Characters\q.png"; TextureManager.LoadTexture(_paths['q']);
            _paths['r'] = KT.KotonoPath + @"Assets\Characters\r.png"; TextureManager.LoadTexture(_paths['r']);
            _paths['s'] = KT.KotonoPath + @"Assets\Characters\s.png"; TextureManager.LoadTexture(_paths['s']);
            _paths['t'] = KT.KotonoPath + @"Assets\Characters\t.png"; TextureManager.LoadTexture(_paths['t']);
            _paths['u'] = KT.KotonoPath + @"Assets\Characters\u.png"; TextureManager.LoadTexture(_paths['u']);
            _paths['v'] = KT.KotonoPath + @"Assets\Characters\v.png"; TextureManager.LoadTexture(_paths['v']);
            _paths['w'] = KT.KotonoPath + @"Assets\Characters\w.png"; TextureManager.LoadTexture(_paths['w']);
            _paths['x'] = KT.KotonoPath + @"Assets\Characters\x.png"; TextureManager.LoadTexture(_paths['x']);
            _paths['y'] = KT.KotonoPath + @"Assets\Characters\y.png"; TextureManager.LoadTexture(_paths['y']);
            _paths['z'] = KT.KotonoPath + @"Assets\Characters\z.png"; TextureManager.LoadTexture(_paths['z']);
            _paths['A'] = KT.KotonoPath + @"Assets\Characters\ua.png"; TextureManager.LoadTexture(_paths['A']);
            _paths['B'] = KT.KotonoPath + @"Assets\Characters\ub.png"; TextureManager.LoadTexture(_paths['B']);
            _paths['C'] = KT.KotonoPath + @"Assets\Characters\uc.png"; TextureManager.LoadTexture(_paths['C']);
            _paths['D'] = KT.KotonoPath + @"Assets\Characters\ud.png"; TextureManager.LoadTexture(_paths['D']);
            _paths['E'] = KT.KotonoPath + @"Assets\Characters\ue.png"; TextureManager.LoadTexture(_paths['E']);
            _paths['F'] = KT.KotonoPath + @"Assets\Characters\uf.png"; TextureManager.LoadTexture(_paths['F']);
            _paths['G'] = KT.KotonoPath + @"Assets\Characters\ug.png"; TextureManager.LoadTexture(_paths['G']);
            _paths['H'] = KT.KotonoPath + @"Assets\Characters\uh.png"; TextureManager.LoadTexture(_paths['H']);
            _paths['I'] = KT.KotonoPath + @"Assets\Characters\ui.png"; TextureManager.LoadTexture(_paths['I']);
            _paths['J'] = KT.KotonoPath + @"Assets\Characters\uj.png"; TextureManager.LoadTexture(_paths['J']);
            _paths['K'] = KT.KotonoPath + @"Assets\Characters\uk.png"; TextureManager.LoadTexture(_paths['K']);
            _paths['L'] = KT.KotonoPath + @"Assets\Characters\ul.png"; TextureManager.LoadTexture(_paths['L']);
            _paths['M'] = KT.KotonoPath + @"Assets\Characters\um.png"; TextureManager.LoadTexture(_paths['M']);
            _paths['N'] = KT.KotonoPath + @"Assets\Characters\un.png"; TextureManager.LoadTexture(_paths['N']);
            _paths['O'] = KT.KotonoPath + @"Assets\Characters\uo.png"; TextureManager.LoadTexture(_paths['O']);
            _paths['P'] = KT.KotonoPath + @"Assets\Characters\up.png"; TextureManager.LoadTexture(_paths['P']);
            _paths['Q'] = KT.KotonoPath + @"Assets\Characters\uq.png"; TextureManager.LoadTexture(_paths['Q']);
            _paths['R'] = KT.KotonoPath + @"Assets\Characters\ur.png"; TextureManager.LoadTexture(_paths['R']);
            _paths['S'] = KT.KotonoPath + @"Assets\Characters\us.png"; TextureManager.LoadTexture(_paths['S']);
            _paths['T'] = KT.KotonoPath + @"Assets\Characters\ut.png"; TextureManager.LoadTexture(_paths['T']);
            _paths['U'] = KT.KotonoPath + @"Assets\Characters\uu.png"; TextureManager.LoadTexture(_paths['U']);
            _paths['V'] = KT.KotonoPath + @"Assets\Characters\uv.png"; TextureManager.LoadTexture(_paths['V']);
            _paths['W'] = KT.KotonoPath + @"Assets\Characters\uw.png"; TextureManager.LoadTexture(_paths['W']);
            _paths['X'] = KT.KotonoPath + @"Assets\Characters\ux.png"; TextureManager.LoadTexture(_paths['X']);
            _paths['Y'] = KT.KotonoPath + @"Assets\Characters\uy.png"; TextureManager.LoadTexture(_paths['Y']);
            _paths['Z'] = KT.KotonoPath + @"Assets\Characters\uz.png"; TextureManager.LoadTexture(_paths['Z']);
            _paths[' '] = KT.KotonoPath + @"Assets\Characters\space.png"; TextureManager.LoadTexture(_paths[' ']);
            _paths['0'] = KT.KotonoPath + @"Assets\Characters\0.png"; TextureManager.LoadTexture(_paths['0']);
            _paths['1'] = KT.KotonoPath + @"Assets\Characters\1.png"; TextureManager.LoadTexture(_paths['1']);
            _paths['2'] = KT.KotonoPath + @"Assets\Characters\2.png"; TextureManager.LoadTexture(_paths['2']);
            _paths['3'] = KT.KotonoPath + @"Assets\Characters\3.png"; TextureManager.LoadTexture(_paths['3']);
            _paths['4'] = KT.KotonoPath + @"Assets\Characters\4.png"; TextureManager.LoadTexture(_paths['4']);
            _paths['5'] = KT.KotonoPath + @"Assets\Characters\5.png"; TextureManager.LoadTexture(_paths['5']);
            _paths['6'] = KT.KotonoPath + @"Assets\Characters\6.png"; TextureManager.LoadTexture(_paths['6']);
            _paths['7'] = KT.KotonoPath + @"Assets\Characters\7.png"; TextureManager.LoadTexture(_paths['7']);
            _paths['8'] = KT.KotonoPath + @"Assets\Characters\8.png"; TextureManager.LoadTexture(_paths['8']);
            _paths['9'] = KT.KotonoPath + @"Assets\Characters\9.png"; TextureManager.LoadTexture(_paths['9']);
            _paths['!'] = KT.KotonoPath + @"Assets\Characters\exclamation.png"; TextureManager.LoadTexture(_paths['!']);
            _paths['?'] = KT.KotonoPath + @"Assets\Characters\interrogation.png"; TextureManager.LoadTexture(_paths['?']);
            _paths['/'] = KT.KotonoPath + @"Assets\Characters\fslash.png"; TextureManager.LoadTexture(_paths['/']);
            _paths['\\'] = KT.KotonoPath + @"Assets\Characters\bslash.png"; TextureManager.LoadTexture(_paths['\\']);
            _paths['.'] = KT.KotonoPath + @"Assets\Characters\dot.png"; TextureManager.LoadTexture(_paths['.']);
            _paths['-'] = KT.KotonoPath + @"Assets\Characters\minus.png"; TextureManager.LoadTexture(_paths['-']);
            _paths['+'] = KT.KotonoPath + @"Assets\Characters\plus.png"; TextureManager.LoadTexture(_paths['+']);
            _paths[':'] = KT.KotonoPath + @"Assets\Characters\colon.png"; TextureManager.LoadTexture(_paths[':']);
        }

        internal Text(string text, Rect dest, Location location, float spacing = 1.0f) 
        {
            _text = text;
            _dest = dest;
            _location = location;
            _spacing = spacing;
        }

        internal void Init()
        {
            Time = Utils.Time.NowS;

            for (int i = 0; i < _text.Length; i++)
            {
                if (!_paths.TryGetValue(_text[i], out string? path))
                {
                    path = _paths[' '];
                }

                switch (_location)
                {
                    case Location.Center:
                        _letters.Add(KT.CreateImage(
                            path, new Rect(
                                _dest.X - _dest.W / 2 * (_text.Length - 1) * _spacing + _dest.W * i * _spacing,
                                _dest.Y,
                                _dest.W,
                                _dest.H
                            )
                        ));
                        break;

                    case Location.TopLeft:
                        _letters.Add(KT.CreateImage(
                            path, new Rect(
                                _dest.X + _dest.W / 2 + _dest.W * i * _spacing,
                                _dest.Y + _dest.H / 2,
                                _dest.W,
                                _dest.H
                            )
                        ));
                        break;

                    default: 
                        break;
                }
            }
        }

        internal void SetText(string text)
        {
            if (text != _text)
            {
                _text = text;
                Clear();
                Init();
            }
        }

        internal void Transform(Rect dest, double time)
        {
            foreach (var letter in _letters)
            {
                letter.Transform(dest, time);
            }
        }

        internal void TransformTo(Rect dest, double time)
        {
            foreach (var letter in _letters)
            {
                letter.TransformTo(dest, time);
            }
        }

        internal void Clear()
        {
            foreach (var letter in _letters)
            {
                KT.DeleteImage(letter);
            }
            _letters.Clear();
        }

        internal void Show()
        {
            foreach (var letter in _letters)
            {
                letter.Show();
            }
        }

        internal void Hide()
        {
            foreach (var letter in _letters)
            {
                letter.Hide();
            }
        }
    }
}
