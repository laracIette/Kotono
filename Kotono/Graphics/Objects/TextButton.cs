using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public class TextButton : Button
    {
        protected readonly Text _text;

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

        public TextButton(Rect dest, Color color, int layer, float fallOff, float cornerSize, string text)
            : base(dest, color, layer, fallOff, cornerSize)
        {
            _text = new Text(text, new Rect(dest.Position, 25.0f, 30.0f), Anchor.Center, Color.White, 0.6f, 2);
            _text.Init();
        }
    }
}
