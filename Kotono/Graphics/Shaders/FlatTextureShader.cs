using Kotono.Graphics.Objects.Managers;

namespace Kotono.Graphics.Shaders
{
    internal class FlatTextureShader()
        : Shader("flatTexture")
    {
        internal override void Update()
        {
            base.Update();

            SetMatrix4("view", CameraManager.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", CameraManager.ActiveCamera.ProjectionMatrix);
        }
    }
}
