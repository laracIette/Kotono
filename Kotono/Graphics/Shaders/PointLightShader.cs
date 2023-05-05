namespace Kotono.Graphics.Shaders
{
    internal class PointLightShader : Shader
    {
        internal PointLightShader() 
            : base("Graphics/Shaders/pointLight.vert", "Graphics/Shaders/pointLight.frag")
        { }

        internal override void Update()
        {
            SetMatrix4("view", KT.GetCameraViewMatrix(0));
            SetMatrix4("projection", KT.GetCameraProjectionMatrix(0));
        }
    }
}
