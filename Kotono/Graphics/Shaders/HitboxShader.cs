using Kotono.Graphics.Objects.Managers;

namespace Kotono.Graphics.Shaders
{
    public class HitboxShader : Shader
    {
        public HitboxShader() 
            : base("hitbox")
        { }

        public override void Update()
        {
            base.Update();

            SetMatrix4("view", CameraManager.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", CameraManager.ActiveCamera.ProjectionMatrix);
        }
    }
}
