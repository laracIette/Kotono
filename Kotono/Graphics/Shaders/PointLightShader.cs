namespace Kotono.Graphics.Shaders
{
    public class PointLightShader : Shader
    {
        public PointLightShader() 
            : base("Graphics/Shaders/pointLight.vert", "Graphics/Shaders/pointLight.frag")
        { }

        public override void Update()
        {
            SetMatrix4("view", KT.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", KT.ActiveCamera.ProjectionMatrix);
        }
    }
}
