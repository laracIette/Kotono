using OpenTK.Graphics.OpenGL4;
using StbTrueTypeSharp;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Texts
{
    internal class Font
    {
        private readonly StbTrueType.stbtt_fontinfo _fontInfo;

        private readonly Dictionary<int, GlyphInfo> _glyphCache = [];

        private float _size;

        internal float Size
        {
            get => _size;
            set
            {
                _size = value;
                Scale = StbTrueType.stbtt_ScaleForPixelHeight(_fontInfo, value);
            }
        }

        internal float Scale { get; set; }

        internal static Font Roboto_Medium { get; } = new(Path.FromAssets(@"Fonts\Roboto-Medium.ttf"));

        private Font(string path)
        {
            byte[] ttf = System.IO.File.ReadAllBytes(path);

            _fontInfo = StbTrueType.CreateFont(ttf, 0);
        }

        public unsafe GlyphInfo GetGlyph(char c)
        {
            int glyphIndex = StbTrueType.stbtt_FindGlyphIndex(_fontInfo, c);

            if (_glyphCache.TryGetValue(glyphIndex, out GlyphInfo value))
            {
                return value;
            }

            int advanceWidth, leftSideBearing;
            int x0, y0, x1, y1;

            // Get glyph metrics
            StbTrueType.stbtt_GetGlyphHMetrics(_fontInfo, glyphIndex, &advanceWidth, &leftSideBearing);
            StbTrueType.stbtt_GetGlyphBitmapBox(_fontInfo, glyphIndex, Scale, Scale, &x0, &y0, &x1, &y1);

            // Create the glyph bitmap
            int width = x1 - x0;
            int height = y1 - y0;

            byte* bitmap = stackalloc byte[width * height];

            StbTrueType.stbtt_MakeGlyphBitmap(_fontInfo, bitmap, width, height, width, Scale, Scale, glyphIndex);

            // Create OpenGL texture
            var texture = new Texture();
            texture.Bind();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.R8, width, height, 0, PixelFormat.Red, PixelType.UnsignedByte, new IntPtr(bitmap));

            // Store the glyph information
            var glyphInfo = new GlyphInfo
            {
                Texture = texture,
                Width = width,
                Height = height,
                BearingX = leftSideBearing * Scale,
                BearingY = y1,
                Advance = advanceWidth * Scale
            };

            _glyphCache[glyphIndex] = glyphInfo;

            return glyphInfo;
        }
    }
}
