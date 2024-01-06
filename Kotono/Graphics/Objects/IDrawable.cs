namespace Kotono.Graphics.Objects
{
    internal interface IDrawable
    {
        public bool IsDraw { get; set; }

        public void Update();

        public void Draw();
    }
}
