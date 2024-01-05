using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Buttons
{
    internal class TextButton(Rect dest, Color color, int layer, float fallOff, float cornerSize, string text) : Button(dest, color, layer, fallOff, cornerSize)
    {
        protected readonly Text _text = new(text, new Rect(dest.Position, 25.0f, 30.0f), Anchor.Center, Color.White, 0.6f, 2);

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
