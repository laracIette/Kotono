using Kotono.Graphics.Textures;

namespace Kotono.Graphics.Objects.Texts
{
    internal struct GlyphInfo
    {
        public Texture Texture { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float BearingX { get; set; }

        public float BearingY { get; set; }

        public float Advance { get; set; }
    }
}
