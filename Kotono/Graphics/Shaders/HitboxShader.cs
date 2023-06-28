namespace Kotono.Graphics.Shaders
{
    internal class HitboxShader : Shader
    {
        internal HitboxShader() 
            : base("Graphics/Shaders/hitbox.vert", "Graphics/Shaders/hitbox.frag")
        { }

        internal override void Update()
        {
            SetMatrix4("view", KT.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", KT.ActiveCamera.ProjectionMatrix);
        }
    }
}
