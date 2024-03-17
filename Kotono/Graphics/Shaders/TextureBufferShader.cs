using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Shaders
{
    internal abstract class TextureBufferShader(string name)
        : Shader(name)
    {
        internal void Draw(int handle)
        {
            Use();

            GL.Disable(EnableCap.DepthTest);

            Texture.Draw(handle);
        }
    }
}
