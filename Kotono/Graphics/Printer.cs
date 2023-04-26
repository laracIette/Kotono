using Kotono.Graphics.Objects;
using Kotono.Graphics.Rects;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class Printer
    {
        private readonly Dictionary<char, string> _paths = new();

        private readonly List<int> _text = new();

        private readonly SRect _dest = new(640, 360, 50, 60);

        public Printer() { }

        public void Init()
        {
            _paths['a'] = @"Assets\Characters\a.png";
            _paths['b'] = @"Assets\Characters\b.png";
            _paths['c'] = @"Assets\Characters\c.png";
            _paths['d'] = @"Assets\Characters\d.png";
            _paths['e'] = @"Assets\Characters\e.png";
            _paths['f'] = @"Assets\Characters\f.png";
            _paths['g'] = @"Assets\Characters\g.png";
            _paths['h'] = @"Assets\Characters\h.png";
            _paths['i'] = @"Assets\Characters\i.png";
            _paths['j'] = @"Assets\Characters\j.png";
            _paths['k'] = @"Assets\Characters\k.png";
            _paths['l'] = @"Assets\Characters\l.png";
            _paths['m'] = @"Assets\Characters\m.png";
            _paths['n'] = @"Assets\Characters\n.png";
            _paths['o'] = @"Assets\Characters\o.png";
            _paths['p'] = @"Assets\Characters\p.png";
            _paths['q'] = @"Assets\Characters\q.png";
            _paths['r'] = @"Assets\Characters\r.png";
            _paths['s'] = @"Assets\Characters\s.png";
            _paths['t'] = @"Assets\Characters\t.png";
            _paths['u'] = @"Assets\Characters\u.png";
            _paths['v'] = @"Assets\Characters\v.png";
            _paths['w'] = @"Assets\Characters\w.png";
            _paths['x'] = @"Assets\Characters\x.png";
            _paths['y'] = @"Assets\Characters\y.png";
            _paths['z'] = @"Assets\Characters\z.png";
            _paths[' '] = @"Assets\Characters\space.png";
        }

        public void Update()
        {

        }


        public void SetText(string text)
        {
            _text.Clear();

            var width = text.Length * _dest.W;

            for (int i = 0; i < text.Length; i++)
            {
                _text.Add(KT.CreateImage(
                    new Image(_paths[text.ToLowerInvariant()[i]], new SRect(_dest.X - width / 2f + _dest.W * i + _dest.W / 2f, _dest.Y, _dest.W, _dest.H))
                ));
            }
        }
    }
}
