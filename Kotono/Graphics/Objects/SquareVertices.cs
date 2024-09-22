using Kotono.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects
{
    internal static class SquareVertices
    {
        private static readonly float[] _vertices =
        [
            // Positions         // Texture Coords
            1.0f,  1.0f, 0.0f,   1.0f, 1.0f, // Top Right
            1.0f, -1.0f, 0.0f,   1.0f, 0.0f, // Bottom Right
           -1.0f, -1.0f, 0.0f,   0.0f, 0.0f, // Bottom Left
           -1.0f,  1.0f, 0.0f,   0.0f, 1.0f  // Top Left 
        ];

        private static readonly int[] _indices =
        [
            0, 1, 3, // First Triangle
            1, 2, 3  // Second Triangle
        ];

        internal static VertexArraySetup VertexArraySetup { get; } = new();

        internal static ElementBufferObject ElementBufferObject { get; } = new();

        static SquareVertices()
        {
            VertexArraySetup.SetData(_vertices, sizeof(float));
            ElementBufferObject.SetData(_indices, sizeof(int));
            VertexArraySetup.VertexArrayObject.SetVertexAttributesLayout(ImageShader.Instance);
        }

        internal static void Draw()
        {
            VertexArraySetup.VertexArrayObject.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
