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
                _text?.TransformTo(new Rect(value.Position, _text.LettersDest.Size));
            }
        }

        public TextButton(Rect dest, Color color, int layer, float fallOff, float cornerSize, string text) 
            : base(dest, color, layer, fallOff, cornerSize)
        {
            _text = new Text(text, new Rect(dest.Position, 25, 30), Anchor.Center, Color.White, 0.6f, 2);
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
