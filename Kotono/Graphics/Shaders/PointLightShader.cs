namespace Kotono.Graphics.Shaders
{
    public class PointLightShader : Shader
    {
        public PointLightShader() 
            : base("Graphics/Shaders/shader.vert", "Graphics/Shaders/pointLight.frag")
        { }

        public override void Update()
        {
            SetMatrix4("view", KT.GetCameraViewMatrix(0));
            SetMatrix4("projection", KT.GetCameraProjectionMatrix(0));
        }
    }
}
