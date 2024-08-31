using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Shaders
{
    internal partial class OutlineShader
    {
        internal void Draw(Texture texture)
        {
            Use();

            GL.Disable(EnableCap.DepthTest);

            texture.Draw();
        }
    }
}
