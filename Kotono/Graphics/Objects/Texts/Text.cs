using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Texts
{
    internal class Text : Object2D, IText
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

        protected readonly List<Image> _letters = []; // TODO: replace by private, and change only position in PrinterText

        private object? _source = null;

        private string _value = string.Empty;

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

                for (int i = 0; i < value.Length; i++)
                {
                    if (!_charactersPath.TryGetValue(value[i], out string? path))
                    {
                        path = _charactersPath[' '];
                    }

                    var position = GetLetterPosition(i, LettersRect.Position, LettersRect.Size);

                    _letters.Add(
                        new Image(Path.FromAssets(path))
                        {
                            Position = position,
                            Size = LettersRect.Size,
                            Color = Color,
                            Layer = Layer
                        }
                    );
                }
            }
        }

        public Anchor Anchor { get; set; } = Anchor.Center;

        public float Spacing { get; set; } = 1.0f;

        public override Rect Rect
        {
            get
            {
                base.Rect.Position = Rect.GetPositionFromAnchor(LettersRect.Position, new Point(LettersRect.BaseSize.X * Value.Length * Spacing, LettersRect.BaseSize.Y), Anchor);
                base.Rect.BaseSize = new Point(LettersRect.BaseSize.X * Value.Length * Spacing, LettersRect.BaseSize.Y);
                return base.Rect;
            }
        }

        internal Rect LettersRect { get; } = Rect.Default;

        public override Point Position
        {
            get => Rect.Position;
            set
            {
                LettersRect.Position = value;

                for (int i = 0; i < _letters.Count; i++)
                {
                    _letters[i].Position = GetLetterPosition(i, LettersRect.Position, LettersRect.Size);
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

        public override Color Color
        {
            get => base.Color;
            set
            {
                base.Color = value;
                foreach (var letter in _letters)
                {
                    letter.Color = value;
                }
            }
        }

        public new bool IsSelected { get; } = false; // TODO: temporary fix

        public new bool IsActive { get; } = false; // TODO: temporary fix

        internal bool IsMouseOn => Rect.Overlaps(Rect, Mouse.Position);

        private Point GetLetterPosition(int index, Point position, Point size)
        {
            float horizontalOffset = Spacing * size.X * (index - (Value.Length - 1) / 2.0f);
            float verticalOffset = size.Y / 2.0f;

            position = Anchor switch
            {
                Anchor.Center => new Point(position.X + horizontalOffset, position.Y),
                Anchor.Top => new Point(position.X + horizontalOffset, position.Y + verticalOffset),
                Anchor.Bottom => new Point(position.X + horizontalOffset, position.Y - verticalOffset),
                Anchor.Left => new Point(position.X + GetLeftOffset(index, size.X), position.Y),
                Anchor.Right => new Point(position.X - GetRightOffset(index, size.X), position.Y),
                Anchor.TopLeft => new Point(position.X + GetLeftOffset(index, size.X), position.Y + verticalOffset),
                Anchor.TopRight => new Point(position.X - GetRightOffset(index, size.X), position.Y + verticalOffset),
                Anchor.BottomLeft => new Point(position.X + GetLeftOffset(index, size.X), position.Y - verticalOffset),
                Anchor.BottomRight => new Point(position.X - GetRightOffset(index, size.X), position.Y - verticalOffset),
                _ => throw new KotonoException($"Text.Init()'s switch on Anchor doesn't handle \"{Anchor}\""),
            };

            return position;
        }

        private float GetLeftOffset(int index, float sizeX) =>
            Spacing * sizeX * (0.5f + index);

        private float GetRightOffset(int index, float sizeX) =>
            -Spacing * sizeX * (1.5f + index - Value.Length);

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

            _letters.Clear();
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
