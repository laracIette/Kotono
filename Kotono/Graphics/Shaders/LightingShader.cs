using Kotono.Utils;

namespace Kotono.Graphics.Shaders
{
    internal class LightingShader : Shader
    {
        internal LightingShader() 
            : base("Graphics/Shaders/lighting.vert", "Graphics/Shaders/lighting.frag")
        { }

        internal override void Update()
        {
            SetInt("numPointLights", KT.GetPointLightsCount());
            SetInt("numSpotLights", KT.GetSpotLightsCount());

            SetMatrix4("view", KT.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", KT.ActiveCamera.ProjectionMatrix);

            SetVector("viewPos", KT.ActiveCamera.Location);

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
