using Kotono.Graphics.Objects;

namespace Kotono.Graphics.Shaders
{
    internal class Object3DShader(string name)
        : Shader(name)
    {
        internal override void Update()
        {
            base.Update();

            SetMatrix4("view", Camera.Active.ViewMatrix);
            SetMatrix4("projection", Camera.Active.ProjectionMatrix);
        }
    }
}
