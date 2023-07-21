using OpenTK.Mathematics;

namespace Kotono.Graphics.Shaders
{
    public class ImageShader : Shader
    {
        public ImageShader()
            : base("Graphics/Shaders/image.vert", "Graphics/Shaders/image.frag")
        { }
    }
}
