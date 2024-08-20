using Kotono.Graphics.Objects.Texts;
using Kotono.Utils.Coordinates;
using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class TextButton : Button
    {
        protected readonly Text _text;

        private readonly TextButtonEventArgs _args = new();

        internal new EventHandler<TextButtonEventArgs>? Pressed { get; set; }

        public override Point Position
        {
            get => base.Position;
            set 
            {
                base.Position = value;
                if (_text != null)
                {
                    _text.Position = value;
                }
            }
        }

        public override bool IsDraw
        {
            get => base.IsDraw;
            set
            {
                base.IsDraw = value;
                _text.IsDraw = value;
            }
        }

        public object? Source
        {
            get => _text.Source;
            set
            {
                _text.Source = value;
                _args.Source = value;
            }
        }

        internal TextButton()
        {
            _text = new Text
            {
                Position = Rect.Position, 
                Size = new Point(25.0f, 30.0f),
                Layer = 2,
                Spacing = 0.6f
            };
        }

        public override void OnPressed()
        {
            Pressed?.Invoke(this, _args);
        }
    }
}
