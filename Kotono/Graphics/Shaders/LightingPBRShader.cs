using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Lights;
using System.Linq;

namespace Kotono.Graphics.Shaders
{
    internal partial class LightingPBRShader
    {
        internal override void Update()
        {
            SetCamLoc(Camera.Active.WorldLocation);
            SetView(Camera.Active.ViewMatrix);
            SetProjection(Camera.Active.ProjectionMatrix);

            var pointLights = ObjectManager.GetObjectsOfType<PointLight>(p => p.IsOn).ToArray();
            SetNumPointLights(pointLights.Length);
            SetPointLights(pointLights);

            var spotLights = ObjectManager.GetObjectsOfType<SpotLight>(p => p.IsOn).ToArray();
            SetNumSpotLights(spotLights.Length);
            SetSpotLights(spotLights);
        }
    }
}
