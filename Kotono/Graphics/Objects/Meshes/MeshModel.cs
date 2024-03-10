using Kotono.Graphics.Objects.Shapes;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class MeshModel
    {
        internal string Path { get; set; } = "";

        internal int VertexArrayObject { get; set; }

        internal int VertexBufferObject { get; set; }

        internal int IndicesCount { get; set; }

        internal Vector Center { get; set; }

        internal Vector[] Vertices { get; set; } = [];

        internal Triangle[] Triangles { get; set; } = [];

        internal void Draw()
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
