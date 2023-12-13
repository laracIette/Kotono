using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public class TextButton(Rect dest, Color color, int layer, float fallOff, float cornerSize, string text) 
        : Button(dest, color, layer, fallOff, cornerSize)
    {
        protected readonly Text _text = new(text, new Rect(dest.Position, 25, 30), Anchor.Center, Color.White, 0.6f, 2);

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

        public override void Init()
        {
            base.Init();
            _text.Init();
        }

        public override void Show()
        {
            base.Show();
            _text.Show();
        }

        public override void Hide()
        {
            base.Hide();
            _text.Hide();
        }
    }
}
