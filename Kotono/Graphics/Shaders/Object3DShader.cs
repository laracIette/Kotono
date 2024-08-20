using Kotono.Graphics.Objects;
using Kotono.Utils;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Shaders
{
    internal class Object3DShader(string name) : Shader(name)
    {
        internal void SetModelMatrix(Matrix4 value) => SetMatrix4("model", value);

        internal void SetViewMatrix(Matrix4 value) => SetMatrix4("view", value);

        internal void SetProjectionMatrix(Matrix4 value) => SetMatrix4("projection", value);

        internal void SetColor(Color value) => SetColor("color", value);

        internal override void Update()
        {
            base.Update();

            SetViewMatrix(Camera.Active.ViewMatrix);
            SetProjectionMatrix(Camera.Active.ProjectionMatrix);
        }
    }
}
