using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;

namespace Kotono.Graphics.Shaders
{
    public class LightingShader : Shader
    {
        public LightingShader()
            : base("lighting")
        { }

        public override void Update()
        {
            base.Update();

            SetInt("numPointLights", PointLight.Count);
            SetInt("numSpotLights", SpotLight.Count);

            SetMatrix4("view", CameraManager.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", CameraManager.ActiveCamera.ProjectionMatrix);

            SetVector("viewPos", CameraManager.ActiveCamera.Location);

            SetInt("material.diffuse", 0);
            SetInt("material.specular", 1);
            SetVector("material.specular", new Vector(0.5f, 0.5f, 0.5f));
            SetFloat("material.shininess", 32.0f);

            SetVector("dirLight.direction", new Vector(-0.2f, -1.0f, -0.3f));
            SetVector("dirLight.ambient", new Vector(0.05f, 0.05f, 0.05f));
            SetVector("dirLight.diffuse", new Vector(0.4f, 0.4f, 0.4f));
            SetVector("dirLight.specular", new Vector(0.5f, 0.5f, 0.5f));
        }
    }
}
