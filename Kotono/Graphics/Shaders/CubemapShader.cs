using Kotono.Graphics.Objects;

namespace Kotono.Graphics.Shaders
{
    internal partial class CubemapShader
    {
        internal override void Update()
        {
            SetView(Camera.Active.ViewMatrix.ClearTranslation());
            SetProjection(Camera.Active.ProjectionMatrix);
        }
    }
}
