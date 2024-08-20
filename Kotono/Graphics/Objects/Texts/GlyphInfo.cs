namespace Kotono.Graphics.Objects.Texts
{
    internal struct GlyphInfo
    {
        public int TextureHandle { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float BearingX { get; set; }

        public float BearingY { get; set; }

        public float Advance { get; set; }
    }
}
