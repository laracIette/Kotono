using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Lights;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System.Linq;

namespace Kotono.Graphics.Shaders
{
    internal partial class NewLightingShader
    {
        private static readonly DirectionalLight _directionalLight = new()
        {
            Direction = Vector.Down,
            Diffuse = new Color(0.4f, 0.4f, 0.4f)
        };

        internal override void Update()
        {
            SetViewPos(Camera.Active.Location);
            SetProjection(Camera.Active.ProjectionMatrix);

            SetDirLight(_directionalLight);

            var pointLights = ObjectManager.GetObjectsOfType<PointLight>(p => p.IsOn).ToArray();
            SetNumPointLights(pointLights.Length);
            SetPointLights(pointLights);

            //var spotLights = ObjectManager.GetObjectsOfType<SpotLight>(s => s.IsOn).ToArray();
            //SetNumPointLights(spotLights.Length);
            //SetSpotLights(spotLights);
        }
    }
}
