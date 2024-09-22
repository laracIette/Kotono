using Kotono.Graphics.Objects;
using Kotono.Graphics.Textures;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Shaders
{
    internal partial class OutlineShader
    {
        internal void Draw(Texture texture)
        {
            Use();

            GL.Disable(EnableCap.DepthTest);

            texture.Use();
            SquareVertices.Draw();
        }
    }
}
