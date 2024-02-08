using Kotono.Settings;
using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;

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
                Text = settings.Text,
                Spacing = 0.6f
            }
        );

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
    }
}
