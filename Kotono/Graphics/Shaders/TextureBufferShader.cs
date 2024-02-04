using Kotono.Graphics.Objects;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Shaders
{
    internal abstract class TextureBufferShader(string name)
        : Shader(name)
    {
        internal void Draw(int textureBuffer)
        {
            GL.BindVertexArray(SquareVertices.VertexArrayObject);
            GL.Disable(EnableCap.DepthTest);

            Use();
            GL.BindTexture(TextureTarget.Texture2D, textureBuffer);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
