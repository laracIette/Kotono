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
                Rect = new Rect(settings.Rect.Position, new Point(25.0f, 30.0f)),
                Layer = 2,
                Source = settings.TextSettings.Source,
                Spacing = 0.6f
            }
        );

        private readonly TextButtonEventArgs _args = new() { Source = settings.TextSettings.Source };

        internal new event EventHandler<TextButtonEventArgs>? Pressed = null;

        public override Rect Rect
        {
            get => base.Rect;
            set
            {
                base.Rect = value;
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
