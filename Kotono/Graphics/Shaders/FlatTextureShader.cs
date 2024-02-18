using Kotono.Graphics.Objects;

namespace Kotono.Graphics.Shaders
{
    internal class FlatTextureShader()
        : Shader("flatTexture")
    {
        internal override void Update()
        {
            base.Update();

            SetMatrix4("view", ObjectManager.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", ObjectManager.ActiveCamera.ProjectionMatrix);
        }
    }
}
