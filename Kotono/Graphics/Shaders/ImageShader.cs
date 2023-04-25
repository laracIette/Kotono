using OpenTK.Mathematics;

namespace Kotono.Graphics.Shaders
{
    public class ImageShader : Shader
    {
        public ImageShader()
            : base("Graphics/Shaders/image.vert", "Graphics/Shaders/image.frag")
        { }

        public override void Update()
        {
            SetMatrix4("view", Matrix4.Identity);
            SetMatrix4("projection", Matrix4.Identity);
        }
    }
}
