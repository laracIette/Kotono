namespace Kotono.Graphics.Shaders
{
    public class HitboxShader : Shader
    {
        public HitboxShader() 
            : base("Graphics/Shaders/hitbox.vert", "Graphics/Shaders/hitbox.frag")
        { }

        public override void Update()
        {
            SetMatrix4("view", KT.GetCameraViewMatrix(0));
            SetMatrix4("projection", KT.GetCameraProjectionMatrix(0));
        }
    }
}
