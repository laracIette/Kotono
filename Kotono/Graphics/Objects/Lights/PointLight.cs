using Kotono.Graphics.Objects.Meshes;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight
    {
        private Vector3 _ambient;

        protected Vector3 _diffuse;

        private Vector3 _specular;

        private float _constant;

        private float _linear;

        private float _quadratic;

        private int _shaderIndex;

        private readonly PointLightMesh _mesh;


        public PointLight(Vector3 position, Vector3 ambient, Vector3 diffuse, Vector3 specular, float constant, float linear, float quadratic)
        {
            _ambient = ambient;
            _diffuse = diffuse;
            _specular = specular;
            _constant = constant;
            _linear = linear;
            _quadratic = quadratic;
            _shaderIndex = KT.GetPointLightsCount();

            _mesh = new PointLightMesh(position);
        }

        public virtual void Update()
        {
            _mesh.Update();
            _mesh.Color = _diffuse;
        }

        public void UpdateIndex()
        {
            _shaderIndex--;
        }

        public void UpdateShaders()
        {
            ShaderManager.Lighting.SetVector3($"pointLights[{_shaderIndex}].position", _mesh.Position);
            ShaderManager.Lighting.SetVector3($"pointLights[{_shaderIndex}].ambient", _ambient);
            ShaderManager.Lighting.SetVector3($"pointLights[{_shaderIndex}].diffuse", _diffuse);
            ShaderManager.Lighting.SetVector3($"pointLights[{_shaderIndex}].specular", _specular);
            ShaderManager.Lighting.SetFloat($"pointLights[{_shaderIndex}].constant", _constant);
            ShaderManager.Lighting.SetFloat($"pointLights[{_shaderIndex}].linear", _linear);
            ShaderManager.Lighting.SetFloat($"pointLights[{_shaderIndex}].quadratic", _quadratic);
        }

        public void Draw()
        {
            _mesh.Draw();
        }
    }
}