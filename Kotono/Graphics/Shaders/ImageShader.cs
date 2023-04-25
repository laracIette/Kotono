namespace Kotono.Graphics.Shaders
{
    public class ImageShader : Shader
    {
        public ImageShader()
            : base("Graphics/Shaders/image.vert", "Graphics/Shaders/image.frag")
        { }

        public override void Update()
        {
            SetMatrix4("view", KT.GetCameraViewMatrix(0));
            SetMatrix4("projection", KT.GetCameraProjectionMatrix(0));
        }
    }
}
