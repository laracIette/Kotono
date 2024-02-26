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

        internal static int VertexArrayObject { get; }

        internal static int VertexBufferObject { get; }

        static SquareVertices()
        {
            // Create vertex array
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            // Create vertex buffer
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * _vertices.Length, _vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        }
    }
}
