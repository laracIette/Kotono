using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Texts
{
    internal class TTFText : Object2D
    {
        public override Shader Shader => ShaderManager.Shaders["image"];

        internal string Text { get; set; } = string.Empty;

        internal Font Font { get; set; } = Font.Roboto_Medium;

        internal float FontSize { get; set; }

        private readonly List<Vertex2D> _vertices = [];

        private readonly List<Texture> _glyphsTextures = [];

        private readonly VertexArraySetup _vertexArraySetup = new();

        internal TTFText()
        {
            // Generate OpenGL buffers
            _vertexArraySetup.VertexArrayObject.Bind();
            _vertexArraySetup.VertexBufferObject.Bind();

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(float) * 4, 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, sizeof(float) * 4, sizeof(float) * 2);
        }

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
                _vertices.Add(new Vertex2D(new Point(xpos, ypos + h), new Point(0.0f, 0.0f))); // bottom-left
                _vertices.Add(new Vertex2D(new Point(xpos + w, ypos + h), new Point(1.0f, 0.0f))); // bottom-right
                _vertices.Add(new Vertex2D(new Point(xpos + w, ypos), new Point(1.0f, 1.0f))); // top-right

                _vertices.Add(new Vertex2D(new Point(xpos, ypos + h), new Point(0.0f, 0.0f))); // bottom-left
                _vertices.Add(new Vertex2D(new Point(xpos + w, ypos), new Point(1.0f, 1.0f))); // top-right
                _vertices.Add(new Vertex2D(new Point(xpos, ypos), new Point(0.0f, 1.0f))); // top-left

                _glyphsTextures.Add(glyph.Texture);

                x += glyph.Advance;
            }

            // Upload vertices to the GPU
            _vertexArraySetup.SetData([.. _vertices], Vertex2D.SizeInBytes);
        }

        public override void Draw()
        {
            Shader.SetColor("color", Color);

            _glyphsTextures.ForEach(t => t.Use());
            print("draw");

            _vertexArraySetup.Bind();
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count);
        }

        internal void ClearText()
        {
            _vertices.Clear();
        }
    }
}
