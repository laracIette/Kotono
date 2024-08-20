using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Shaders
{
    internal class LightingShader() 
        : Object3DShader("lighting")
    {
        internal override void Update()
        {
            base.Update();

            SetVector("viewPos", Camera.Active.Location);

            SetInt("material.diffuse", 0);
            SetInt("material.specular", 1);
            SetVector("material.specular", new Vector(0.5f, 0.5f, 0.5f));
            SetFloat("material.shininess", 32.0f);

            SetVector("dirLight.direction", new Vector(-0.2f, -1.0f, -0.3f));
            SetVector("dirLight.ambient", new Vector(0.05f, 0.05f, 0.05f));
            SetVector("dirLight.diffuse", new Vector(0.4f, 0.4f, 0.4f));
            SetVector("dirLight.specular", new Vector(0.5f, 0.5f, 0.5f));

            var pointLights = ObjectManager.PointLights;

            SetInt("numPointLights", pointLights.Length);

            for (int i = 0; i < pointLights.Length; i++)
            {
                SetVector($"pointLights[{i}].location", pointLights[i].WorldLocation);
                SetColor($"pointLights[{i}].ambient", pointLights[i].Ambient);
                SetColor($"pointLights[{i}].diffuse", pointLights[i].Diffuse);
                SetColor($"pointLights[{i}].specular", pointLights[i].Specular);
                SetFloat($"pointLights[{i}].constant", pointLights[i].Constant);
                SetFloat($"pointLights[{i}].linear", pointLights[i].Linear);
                SetFloat($"pointLights[{i}].quadratic", pointLights[i].Quadratic);
                SetFloat($"pointLights[{i}].power", pointLights[i].Power);
            }

            var spotLights = ObjectManager.SpotLights;

            SetInt("numSpotLights", spotLights.Length);

            for (int i = 0; i < spotLights.Length; i++)
            {
                SetFloat($"spotLights[{i}].cutOff", Math.Cos(Math.Rad(spotLights[i].CutOffAngle)));
                SetFloat($"spotLights[{i}].outerCutOff", Math.Cos(Math.Rad(spotLights[i].OuterCutOffAngle)));
                SetVector($"spotLights[{i}].location", spotLights[i].WorldLocation);
                SetVector($"spotLights[{i}].direction", spotLights[i].Transform.Forward);
                SetColor($"spotLights[{i}].ambient", Color.Black);
                SetColor($"spotLights[{i}].diffuse", spotLights[i].Diffuse);
                SetColor($"spotLights[{i}].specular", Color.White);
                SetFloat($"spotLights[{i}].constant", 1.0f);
                SetFloat($"spotLights[{i}].linear", 0.09f);
                SetFloat($"spotLights[{i}].quadratic", 0.032f);
                SetFloat($"spotLights[{i}].power", spotLights[i].Power);
            }
        }
    }
}
