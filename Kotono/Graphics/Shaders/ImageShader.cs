using OpenTK.Mathematics;

namespace Kotono.Graphics.Shaders
{
    internal class ImageShader : Shader
    {
        internal ImageShader()
            : base("Graphics/Shaders/image.vert", "Graphics/Shaders/image.frag")
        { }

        internal override void Update()
        {
            SetMatrix4("view", Matrix4.Identity);
            SetMatrix4("projection", Matrix4.Identity);
        }
    }
}
