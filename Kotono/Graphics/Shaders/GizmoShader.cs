using Kotono.Graphics.Objects;

namespace Kotono.Graphics.Shaders
{
    internal partial class GizmoShader
    {
        internal override void Update()
        {
            SetView(Camera.Active.ViewMatrix);
            SetProjection(Camera.Active.ProjectionMatrix);
        }
    }
}
