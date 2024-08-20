using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal static class SquareVertices
    {
        private static readonly float[] _vertices =
        [
            // locations   // texCoords
            -1.0f,
            1.0f,
            0.0f,
            1.0f,
            -1.0f,
            -1.0f,
            0.0f,
            0.0f,
            1.0f,
            -1.0f,
            1.0f,
            0.0f,

            -1.0f,
            1.0f,
            0.0f,
            1.0f,
            1.0f,
            -1.0f,
            1.0f,
            0.0f,
            1.0f,
            1.0f,
            1.0f,
            1.0f
        ];

        internal static VertexArraySetup VertexArraySetup { get; } = new();

        static SquareVertices()
        {
            VertexArraySetup.VertexArrayObject.Bind();
            VertexArraySetup.VertexBufferObject.SetData(_vertices, sizeof(float));

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        }

        internal static void Draw()
        {
            VertexArraySetup.VertexArrayObject.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
