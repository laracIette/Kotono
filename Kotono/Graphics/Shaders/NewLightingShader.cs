using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Lights;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

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

            var pointLights = ObjectManager.PointLights;
            SetNumPointLights(pointLights.Length);
            SetPointLights(pointLights);

            //var spotLights = ObjectManager.SpotLights;

            //SetInt("numSpotLights", spotLights.Length);

            //for (int i = 0; i < spotLights.Length; i++)
            //{
            //    SetFloat($"spotLights[{i}].cutOff", Math.Cos(Math.Rad(spotLights[i].CutOffAngle)));
            //    SetFloat($"spotLights[{i}].outerCutOff", Math.Cos(Math.Rad(spotLights[i].OuterCutOffAngle)));
            //    SetVector($"spotLights[{i}].location", spotLights[i].WorldLocation);
            //    SetVector($"spotLights[{i}].direction", spotLights[i].Transform.Forward);
            //    SetColor($"spotLights[{i}].ambient", Color.Black);
            //    SetColor($"spotLights[{i}].diffuse", spotLights[i].Diffuse);
            //    SetColor($"spotLights[{i}].specular", Color.White);
            //    SetFloat($"spotLights[{i}].constant", 1.0f);
            //    SetFloat($"spotLights[{i}].linear", 0.09f);
            //    SetFloat($"spotLights[{i}].quadratic", 0.032f);
            //    SetFloat($"spotLights[{i}].power", spotLights[i].Power);
            //}
        }
    }
}
