using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Shaders
{
    internal class TextureBufferShader(string name)
        : Shader(name)
    {
        internal void Draw(Texture texture)
        {
            Use();

            GL.Disable(EnableCap.DepthTest);

            texture.Draw();
        }
    }
}
