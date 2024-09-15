using Kotono.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics
{
    internal sealed class VertexArrayObject : IDisposable
    {
        private readonly int _handle = GL.GenVertexArray();

        internal void Bind() => GL.BindVertexArray(_handle);

        internal static void Unbind() => GL.BindVertexArray(0);

        internal void SetVertexAttributesLayout(Shader shader)
        {
            Bind();
            shader.SetVertexAttributesLayout();
        }

        public void Dispose()
        {
            Unbind();
            GL.DeleteVertexArray(_handle);
        }
    }
}
