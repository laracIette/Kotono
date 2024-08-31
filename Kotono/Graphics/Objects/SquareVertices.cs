using Kotono.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal static class SquareVertices
    {
        private static readonly float[] _vertices =
        [
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
            VertexArraySetup.SetData(_vertices, sizeof(float));
            ImageShader.Instance.SetVertexAttributesData();
        }

        internal static void Draw()
        {
            VertexArraySetup.Bind();
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
