using Kotono.Graphics.Objects.Texts;
using Kotono.Utils.Coordinates;
using System;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class TextButton(TextButtonSettings settings)
        : Button(settings)
    {
        protected readonly Text _text = new(
            new TextSettings
            {
                Dest = new Rect(settings.Dest.Position, 25.0f, 30.0f),
                Layer = 2,
                Text = settings.TextSettings.Text,
                Spacing = 0.6f
            }
        );

        private readonly TextButtonEventArgs _args = new() { Text = settings.TextSettings.Text };

        internal new event EventHandler<TextButtonEventArgs>? Pressed = null;

        public override Rect Dest
        {
            get => base.Dest;
            set
            {
                base.Dest = value;
                if (_text != null)
                {
                    _text.Position = value.Position;
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

        public override void OnPressed()
        {
            Pressed?.Invoke(this, _args);
        }
    }
}
