using Kotono.Graphics.Objects;
using Kotono.Graphics.Rects;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class Printer
    {
        private readonly Dictionary<char, string> _paths = new();

        private readonly List<int> _letters = new();

        private readonly SRect _dest = new(640, 360, 50, 60);

        private SRect _oldDest = new(640, 360, 50, 60);

        public Printer() { }

        public void Init()
        {
            _paths['a'] = KT.KotonoPath + @"Assets\Characters\a.png";
            _paths['b'] = KT.KotonoPath + @"Assets\Characters\b.png";
            _paths['c'] = KT.KotonoPath + @"Assets\Characters\c.png";
            _paths['d'] = KT.KotonoPath + @"Assets\Characters\d.png";
            _paths['e'] = KT.KotonoPath + @"Assets\Characters\e.png";
            _paths['f'] = KT.KotonoPath + @"Assets\Characters\f.png";
            _paths['g'] = KT.KotonoPath + @"Assets\Characters\g.png";
            _paths['h'] = KT.KotonoPath + @"Assets\Characters\h.png";
            _paths['i'] = KT.KotonoPath + @"Assets\Characters\i.png";
            _paths['j'] = KT.KotonoPath + @"Assets\Characters\j.png";
            _paths['k'] = KT.KotonoPath + @"Assets\Characters\k.png";
            _paths['l'] = KT.KotonoPath + @"Assets\Characters\l.png";
            _paths['m'] = KT.KotonoPath + @"Assets\Characters\m.png";
            _paths['n'] = KT.KotonoPath + @"Assets\Characters\n.png";
            _paths['o'] = KT.KotonoPath + @"Assets\Characters\o.png";
            _paths['p'] = KT.KotonoPath + @"Assets\Characters\p.png";
            _paths['q'] = KT.KotonoPath + @"Assets\Characters\q.png";
            _paths['r'] = KT.KotonoPath + @"Assets\Characters\r.png";
            _paths['s'] = KT.KotonoPath + @"Assets\Characters\s.png";
            _paths['t'] = KT.KotonoPath + @"Assets\Characters\t.png";
            _paths['u'] = KT.KotonoPath + @"Assets\Characters\u.png";
            _paths['v'] = KT.KotonoPath + @"Assets\Characters\v.png";
            _paths['w'] = KT.KotonoPath + @"Assets\Characters\w.png";
            _paths['x'] = KT.KotonoPath + @"Assets\Characters\x.png";
            _paths['y'] = KT.KotonoPath + @"Assets\Characters\y.png";
            _paths['z'] = KT.KotonoPath + @"Assets\Characters\z.png";
            _paths['A'] = KT.KotonoPath + @"Assets\Characters\ua.png";
            _paths['B'] = KT.KotonoPath + @"Assets\Characters\ub.png";
            _paths['C'] = KT.KotonoPath + @"Assets\Characters\uc.png";
            _paths['D'] = KT.KotonoPath + @"Assets\Characters\ud.png";
            _paths['E'] = KT.KotonoPath + @"Assets\Characters\ue.png";
            _paths['F'] = KT.KotonoPath + @"Assets\Characters\uf.png";
            _paths['G'] = KT.KotonoPath + @"Assets\Characters\ug.png";
            _paths['H'] = KT.KotonoPath + @"Assets\Characters\uh.png";
            _paths['I'] = KT.KotonoPath + @"Assets\Characters\ui.png";
            _paths['J'] = KT.KotonoPath + @"Assets\Characters\uj.png";
            _paths['K'] = KT.KotonoPath + @"Assets\Characters\uk.png";
            _paths['L'] = KT.KotonoPath + @"Assets\Characters\ul.png";
            _paths['M'] = KT.KotonoPath + @"Assets\Characters\um.png";
            _paths['N'] = KT.KotonoPath + @"Assets\Characters\un.png";
            _paths['O'] = KT.KotonoPath + @"Assets\Characters\uo.png";
            _paths['P'] = KT.KotonoPath + @"Assets\Characters\up.png";
            _paths['Q'] = KT.KotonoPath + @"Assets\Characters\uq.png";
            _paths['R'] = KT.KotonoPath + @"Assets\Characters\ur.png";
            _paths['S'] = KT.KotonoPath + @"Assets\Characters\us.png";
            _paths['T'] = KT.KotonoPath + @"Assets\Characters\ut.png";
            _paths['U'] = KT.KotonoPath + @"Assets\Characters\uu.png";
            _paths['V'] = KT.KotonoPath + @"Assets\Characters\uv.png";
            _paths['W'] = KT.KotonoPath + @"Assets\Characters\uw.png";
            _paths['X'] = KT.KotonoPath + @"Assets\Characters\ux.png";
            _paths['Y'] = KT.KotonoPath + @"Assets\Characters\uy.png";
            _paths['Z'] = KT.KotonoPath + @"Assets\Characters\uz.png";
            _paths[' '] = KT.KotonoPath + @"Assets\Characters\space.png";
            _paths['0'] = KT.KotonoPath + @"Assets\Characters\0.png";
            _paths['1'] = KT.KotonoPath + @"Assets\Characters\1.png";
            _paths['2'] = KT.KotonoPath + @"Assets\Characters\2.png";
            _paths['3'] = KT.KotonoPath + @"Assets\Characters\3.png";
            _paths['4'] = KT.KotonoPath + @"Assets\Characters\4.png";
            _paths['5'] = KT.KotonoPath + @"Assets\Characters\5.png";
            _paths['6'] = KT.KotonoPath + @"Assets\Characters\6.png";
            _paths['7'] = KT.KotonoPath + @"Assets\Characters\7.png";
            _paths['8'] = KT.KotonoPath + @"Assets\Characters\8.png";
            _paths['9'] = KT.KotonoPath + @"Assets\Characters\9.png";
            _paths['!'] = KT.KotonoPath + @"Assets\Characters\exclamation.png";
            _paths['?'] = KT.KotonoPath + @"Assets\Characters\interrogation.png";
            _paths['/'] = KT.KotonoPath + @"Assets\Characters\fslash.png";
            _paths['\\'] = KT.KotonoPath + @"Assets\Characters\bslash.png";
            _paths['.'] = KT.KotonoPath + @"Assets\Characters\dot.png";
        }

        public void Update()
        {
            if (_dest != _oldDest)   
            {
                _oldDest = _dest;    
                                     
                for (int i = 0; i < _letters.Count; i++)      
                {
                    KT.SetImageX(_letters[i], _dest.X - _letters.Count * _dest.W / 2f + _dest.W * i + _dest.W / 2f);
                    KT.SetImageY(_letters[i], _dest.Y);
                    KT.SetImageW(_letters[i], _dest.W);
                    KT.SetImageH(_letters[i], _dest.H);
                }
            }
        }

        public void SetText(string text)
        {
            foreach (var letter in _letters)
            {
                KT.DeleteImage(letter);
            }
            _letters.Clear();

            for (int i = 0; i < text.Length; i++)
            {
                if (!_paths.TryGetValue(text[i], out string? value))
                {
                    value = _paths[' '];
                }

                _letters.Add(KT.CreateImage(
                    new Image(value, new SRect(
                        _dest.X - text.Length * _dest.W / 2f + _dest.W * i + _dest.W / 2f,
                        _dest.Y, _dest.W, _dest.H))
                ));
            }
        }
    }
}
