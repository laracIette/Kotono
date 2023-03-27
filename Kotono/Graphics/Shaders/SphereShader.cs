namespace Kotono.Graphics.Shaders
{
    public class SphereShader : Shader
    {
        public SphereShader() 
            : base("Graphics/Shaders/sphere.vert", "Graphics/Shaders/sphere.frag")
        { }

        public override void Update()
        {
            SetMatrix4("view", KT.GetCameraViewMatrix(0));
            SetMatrix4("projection", KT.GetCameraProjectionMatrix(0));
            SetVector3("cameraRight", KT.GetCameraRight(0));
            SetVector3("cameraUp", KT.GetCameraUp(0));
        }
    }
}
