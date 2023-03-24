using OpenTK.Mathematics;

namespace Kotono.Graphics.Shaders
{
    public class LightingShader : Shader
    {
        public LightingShader() 
            : base("Graphics/Shaders/shader.vert", "Graphics/Shaders/lighting.frag")
        { }

        public override void Update()
        {
            SetInt("numLights", KT.GetPointLightsCount());

            SetMatrix4("view", KT.GetCameraViewMatrix(0));
            SetMatrix4("projection", KT.GetCameraProjectionMatrix(0));

            SetVector3("viewPos", KT.GetCameraPosition(0));

            SetInt("material.diffuse", 0);
            SetInt("material.specular", 1);
            SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            SetFloat("material.shininess", 32.0f);

            SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            SetVector3("spotLight.position", KT.GetCameraPosition(0));
            SetVector3("spotLight.direction", KT.GetCameraFront(0));
            SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            SetFloat("spotLight.constant", 1.0f);
            SetFloat("spotLight.linear", 0.09f);
            SetFloat("spotLight.quadratic", 0.032f);
        }
    }
}
