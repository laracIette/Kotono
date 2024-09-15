using Kotono.Graphics.Textures;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Texts
{
    internal class Text : Object2D, IText
    {
        private static readonly Dictionary<char, string> _characterPaths = new()
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

        private Image[] _letters = [];

        private object? _source = null;

        private string _value = string.Empty;

        private Color _lettersColor = Color.White;

        public virtual object? Source
        {
            get => _source;
            set
            {
                _source = value;

                string newValue = value?.ToString() ?? string.Empty;

                if (newValue != Value)
                {
                    Value = newValue;
                }
            }
        }

        public string Value
        {
            get => _value;
            private set
            {
                _value = value;

                Clear();

                _letters = new Image[value.Length];

                for (int i = 0; i < value.Length; i++)
                {
                    if (!_characterPaths.TryGetValue(value[i], out string? path))
                    {
                        path = _characterPaths[' '];
                    }

                    _letters[i] = new Image
                    {
                        Texture = new ImageTexture(Path.FromAssets(path)),
                        RelativePosition = GetLetterPosition(i, LettersRect.RelativeSize),
                        RelativeSize = LettersRect.RelativeSize,
                        Color = LettersColor,
                        Layer = Layer,
                        Parent = this
                    };
                }
            }
        }

        public float Spacing { get; set; } = 1.0f;

        public override Point RelativeSize => new(LettersRect.RelativeSize.X * Value.Length * Spacing, LettersRect.RelativeSize.Y);

        internal Rect LettersRect { get; } = Rect.Default;

        public Color LettersColor
        {
            get => _lettersColor;
            set
            {
                _lettersColor = value;
                foreach (var letter in _letters)
                {
                    letter.Color = value;
                }
            }
        }

        private Point GetLetterPosition(int index, Point size)
        {
            float centerOffset = Spacing * size.X * Math.Half(index - (Value.Length - 1));
            float verticalOffset = Math.Half(size.Y);

            return new Point(
                  (Anchor & Anchor.Left) == Anchor.Left ? GetLeftOffset(index, size.X)
                : (Anchor & Anchor.Right) == Anchor.Right ? GetRightOffset(index, size.X)
                : centerOffset,
                  (Anchor & Anchor.Top) == Anchor.Top ? verticalOffset
                : (Anchor & Anchor.Bottom) == Anchor.Bottom ? -verticalOffset
                : 0.0f
            );
        }

        private float GetLeftOffset(int index, float sizeX)
            => Spacing * sizeX * (0.5f + index);

        private float GetRightOffset(int index, float sizeX)
            => -Spacing * sizeX * (Value.Length - index - 0.5f);

        private void DisposeLetters()
        {
            foreach (var letter in _letters)
            {
                letter.Dispose();
            }
        }

        internal void Clear()
        {
            DisposeLetters();

            _letters = [];
        }

        public override void Dispose()
        {
            LettersRect.Dispose();

            DisposeLetters();

            base.Dispose();
        }

        public override string ToString() => Value;
    }
}
