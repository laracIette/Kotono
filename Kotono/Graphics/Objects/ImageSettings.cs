using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public class ImageSettings 
    {
        public string Path { get; set; } = "";

        public Rect Dest { get; set; } = Rect.Zero;

        public Color Color { get; set; } = Color.White;

        public int Layer { get; set; } = 0;
    }
}
