using Kotono.Graphics.Shaders;
using Kotono.Graphics.Textures;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Texts
{
    internal sealed class TTFText : Object2D
    {
        public override Shader Shader => ImageShader.Instance;

        internal string Text { get; set; } = string.Empty;

        internal Font Font { get; set; } = Font.Roboto_Medium;

        internal float FontSize { get; set; }

        private readonly List<Vertex2D> _vertices = [];

        private readonly List<Texture> _glyphsTextures = [];

        private readonly VertexArraySetup _vertexArraySetup = new();

        internal void AddText(string text, Point position)
        {
            float x = position.X;
            float y = position.Y;

            foreach (char c in text)
            {
                Font.Size = FontSize;
                var glyph = Font.GetGlyph(c);

                float xpos = x + glyph.BearingX;
                float ypos = y - (glyph.Height - glyph.BearingY);

                float w = glyph.Width;
                float h = glyph.Height;

                // Add vertices for the glyph quad
                _vertices.Add(new Vertex2D { Pos = new Point(xpos, ypos + h), TexCoords = new Point(0.0f, 0.0f) }); // bottom-left
                _vertices.Add(new Vertex2D { Pos = new Point(xpos + w, ypos + h), TexCoords = new Point(1.0f, 0.0f) }); // bottom-right
                _vertices.Add(new Vertex2D { Pos = new Point(xpos + w, ypos), TexCoords = new Point(1.0f, 1.0f) }); // top-right

                _vertices.Add(new Vertex2D { Pos = new Point(xpos, ypos + h), TexCoords = new Point(0.0f, 0.0f) }); // bottom-left
                _vertices.Add(new Vertex2D { Pos = new Point(xpos + w, ypos), TexCoords = new Point(1.0f, 1.0f) }); // top-right
                _vertices.Add(new Vertex2D { Pos = new Point(xpos, ypos), TexCoords = new Point(0.0f, 1.0f) }); // top-left

                _glyphsTextures.Add(glyph.Texture);

                x += glyph.Advance;
            }

            // Upload vertices to the GPU
            _vertexArraySetup.SetData([.. _vertices], Vertex2D.SizeInBytes);
            _vertexArraySetup.VertexArrayObject.SetVertexAttributesLayout(ImageShader.Instance);
        }

        public override void UpdateShader()
        {
            if (Shader is ImageShader imageShader)
            {
                imageShader.SetColor(Color);
            }
        }

        public override void Draw()
        {
            _glyphsTextures.ForEach(t => t.Use());

            _vertexArraySetup.VertexArrayObject.Bind();
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count);
        }

        internal void ClearText()
        {
            _vertices.Clear();
        }
    }
}
